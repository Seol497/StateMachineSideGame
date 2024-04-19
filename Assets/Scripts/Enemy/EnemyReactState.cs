using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReactState : EnemyState
{
    public EnemyReactState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isSleep = true;
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
            switch (Random.Range(0, 7))
            {
                case 0:
                    stateMachine.ChangeState(enemy.reactState);
                    break;
                case 1:
                case 2:
                    stateMachine.ChangeState(enemy.idleState);
                    break ;
                case 3:
                case 4:
                case 5:
                case 6:
                    stateMachine.ChangeState(enemy.moveState);
                    break ;
            }
            enemy.isSleep = false;
        }
    }
}
