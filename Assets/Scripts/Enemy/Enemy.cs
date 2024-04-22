using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Attack details")]
    public float damage;

    [Header("Anim Duration")]
    public float idleDuration = 3;
    public float moveDuration = 7;
    public float speedUp = 2;
    public int angle = 0;

    [Header("bool")]
    public bool React = true;
    public bool Attack = true;
    public bool Skill = true;
    public bool Shoot = true;                       

    [Header("상태 확인")]
    public bool isPlayerDetected = false;
    public bool isAttackAlready = false;
    public bool isAttacking = false;
    public bool isSleep = false;

    [Header("Skill")]
    public GameObject bringerSkill;

    public Transform playerPos;

    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyIdleState idleState { get; private set; }
    public EnemyMoveState moveState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    public EnemySkillState skillState { get; private set; }
    public EnemyReactState reactState { get; private set; }
    public EnemyHitState hitState { get; private set; }
    public EnemyDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        moveState = new EnemyMoveState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        skillState = new EnemySkillState(this, stateMachine, "Skill");
        reactState = new EnemyReactState(this, stateMachine, "React");
        hitState = new EnemyHitState(this, stateMachine, "Hit");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider, true);
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void SpawnSkill(GameObject Skill)
    {
        Instantiate(Skill);
    }

    public override void GetDamage(float num)
    {
        base.GetDamage(num);
        if (isDead)
            stateMachine.ChangeState(deadState);
        else
            stateMachine.ChangeState(hitState);
    }

    public override void SetVelocity(float _xVelocity, float _yVelocity)
    {
        base.SetVelocity(_xVelocity * angle, _yVelocity);
    }

    public override bool IsGroundDetected() => base.IsGroundDetected();
    public override bool IsWallDetected() => base.IsWallDetected();

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
