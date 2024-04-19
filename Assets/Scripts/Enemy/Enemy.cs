using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    protected LayerMask whatIPlayer;

    [Header("Anim Duration")]
    public float idleDuration = 3;
    public float moveDuration = 7;
    public float speedUp = 2;
    public int angle = 0;

    [Header("bool")]
    public bool isPlayerDetected = false;
    public bool isAttackAlready = false;
    public bool isAttacking = false;
    public bool isSleep = false;

    public Transform playerPos;

    #region States
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyIdleState idleState { get; private set; }
    public EnemyMoveState moveState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    public EnemyReactState reactState { get; private set; }
    public EnemyHitState hitState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        moveState = new EnemyMoveState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        reactState = new EnemyReactState(this, stateMachine, "React");
        hitState = new EnemyHitState(this, stateMachine, "Hit");
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

    //protected void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    //    }
    //    if (collision.collider.CompareTag("Enemy"))
    //    {
    //        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    //    }
    //}

}
