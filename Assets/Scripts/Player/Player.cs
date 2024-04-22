using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Player : Entity
{


    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float attackDamage;
    public float damage;

    public bool isBusy { get; private set; }


    [Header("Move info")]
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("Bool")]
    public bool isHit;





    #region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; }
    public PlayerHitState hitState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    
    #endregion


    protected override void Awake()
    {

        base.Awake();


        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        hitState = new PlayerHitState(this, stateMachine, "Hit");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");

        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }



    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();


        CheckForDashInput();

       
    }

    public override void GetDamage(float num)
    {
        base.GetDamage(num);
        dashDir = 0;
        if (isDead)
            stateMachine.ChangeState(deadState);
        else
            stateMachine.ChangeState(hitState);
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
      
        yield return new WaitForSeconds(_seconds);
      
        isBusy = false;
    }

    public override void SetVelocity(float _xVelocity, float _yVelocity)
    {
        base.SetVelocity(_xVelocity, _yVelocity);
    }


    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();




    private void CheckForDashInput()
    {

        if (IsWallDetected() && isHit && isDead)
            return;

        dashUsageTimer -= Time.deltaTime;


        if(Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer <0 &&!isHit && !isDead)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

           if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
           
    }

 





}
