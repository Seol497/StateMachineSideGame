using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }
    private bool isDetected;

    public override void Enter()
    {
        base.Enter();


        stateTimer = enemy.moveDuration;
        if (enemy.transform.rotation.y == -180)
            x = -1;
        else if (enemy.transform.rotation.y == 0)
            x = 1;
        else
            x = 1;
        if ((int)Random.Range(0, 5) == 0 && !enemy.isPlayerDetected)
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

        if (enemy.isPlayerDetected && !isDetected)
        {
            isDetected = true;
            enemy.moveSpeed += enemy.speedUp;
            stateTimer = enemy.moveDuration;
        }
        if(!enemy.isPlayerDetected && isDetected)
        {
            isDetected = false;
            enemy.moveSpeed -= enemy.speedUp;
            if (Random.Range(0, 5) == 0)
                x *= -1;
        }
        if (enemy.isAttackAlready)
            stateMachine.ChangeState(enemy.attackState);

        if (stateTimer < 0)
        {
            x = 0;           
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
