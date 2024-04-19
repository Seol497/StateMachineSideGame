using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {

    }
    public override void Enter()
    {
        stateTimer = 1.5f;
        enemy.ZeroVelocity();
        enemy.isAttacking = true;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        AnimatorStateInfo stateInfo = enemy.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1f)
        {
            enemy.isAttacking = false;
            if (enemy.isAttackAlready)
            {
                stateMachine.ChangeState(enemy.attackState);
                return;
            }
            else
            {
                if (enemy.isPlayerDetected)
                    stateMachine.ChangeState(enemy.moveState);
                else
                    stateMachine.ChangeState(enemy.idleState);
                return;
            }
        }
    }
}
