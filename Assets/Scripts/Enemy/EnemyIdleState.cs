using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : 
        base(_enemyBase, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.ZeroVelocity();
        stateTimer = enemy.idleDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isAttackAlready && enemy.Attack)
        {
            stateMachine.ChangeState(enemy.attackState);
            return;
        }
        if (stateTimer < 0 && enemy.React)
            stateMachine.ChangeState(enemy.reactState);
        else
            stateMachine.ChangeState(enemy.moveState);

        if (enemy.isPlayerDetected)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
