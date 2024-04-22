using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.isHit = true;
        stateTimer = 0.5f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(-player.moveSpeed * player.facingDir / 4, rb.velocity.y);
        if (stateTimer < 0)
        {
            player.isHit = false;
            stateMachine.ChangeState(player.idleState);
        }
    }
}
