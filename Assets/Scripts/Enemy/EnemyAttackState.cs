using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {

    }

    private int attackCount = 0;

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
            attackCount++;
            enemy.isAttacking = false;
            if (enemy.isAttackAlready)
            {
                stateMachine.ChangeState(enemy.attackState);
            }
            else
            {
                if (enemy.isPlayerDetected)
                {
                    if (attackCount >= 3 && enemy.Skill)
                        stateMachine.ChangeState(enemy.skillState);
                    else
                    stateMachine.ChangeState(enemy.moveState);
                }
                else
                    stateMachine.ChangeState(enemy.idleState);
            }
        }
    }
}
