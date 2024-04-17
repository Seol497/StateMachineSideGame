using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    private bool isPlayerDetected;
    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.moveDuration;
        if (enemy.transform.rotation.y == 180)
            x = 1;
        else if (enemy.transform.rotation.y == 0)
            x = -1;
        else
            x = -1;
        if ((int)Random.Range(0, 4) == 0)
            x *= -1;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(x * enemy.moveSpeed, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            x *= -1;

        if (stateTimer < 0 && !isPlayerDetected)
        {
            x = 0;
           stateMachine.ChangeState(enemy.idleState);
        }
        if (enemy.IsPlayerDetected())
        {
            isPlayerDetected = true;
            enemy.moveSpeed *= 1.5f;
        }
        if (!enemy.IsPlayerDetected() && isPlayerDetected)
        {
            
            isPlayerDetected = false;
        }
            


    }
}
