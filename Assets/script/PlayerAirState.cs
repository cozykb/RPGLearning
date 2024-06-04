using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() || rb.velocity.y > 0) //这里用触点法做修改
            anim.SetFloat("yVelocity", yVelocity);
        else
            stateMachine.ChangeState(player.playerIdleState);
        
        player.setVelocity(xInput * moveSpeed, rb.velocity.y);
    }
}
