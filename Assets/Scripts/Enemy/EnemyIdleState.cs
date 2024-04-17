using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
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


        if (x != 0)
            stateMachine.ChangeState(enemy.moveState);

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
