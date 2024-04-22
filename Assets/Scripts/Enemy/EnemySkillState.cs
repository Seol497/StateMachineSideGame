using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillState : EnemyState
{
    public EnemySkillState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }
    float maxHp;

    int skillCount = 1;
    public override void Enter()
    {
        base.Enter();
        maxHp = enemy.hp;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.hp < maxHp / 4)
        {
            maxHp = enemy.hp;
            skillCount++;
        }
        AnimatorStateInfo stateInfo = enemy.anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1f)
        {
            enemy.SpawnSkill(enemy.bringerSkill);
            if (enemy.isAttackAlready)
                stateMachine.ChangeState(enemy.attackState);
            else
            {
                if (enemy.isPlayerDetected)
                {
                    if (Random.Range(0, 7) == 3 && enemy.Skill)
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
