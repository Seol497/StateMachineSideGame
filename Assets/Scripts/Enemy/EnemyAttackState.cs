using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        enemy.ZeroVelocity();
        enemy.isAttacking = true;
        stateTimer = 1.5f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            enemy.isAttacking = false;
            enemy.isAttackAlready = false;
            if (enemy.isPlayerDetected)
                stateMachine.ChangeState(enemy.moveState);
            else
                stateMachine.ChangeState(enemy.idleState);
        }
    }
}
