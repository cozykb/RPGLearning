using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    // Start is called before the first frame update
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.setVelocity(xInput, rb.velocity.y * 0.6f, 10);
    }

    public override void Update( )
    {   
        base.Update();
        Debug.Log($"wall state {player.WallAttachedDirection}, xInput {xInput}");
        if ( xInput * player.WallAttachedDirection <= 0)
        {   
            Debug.Log("wall state out 1");
            stateMachine.ChangeState(player.playerFallState);
        }
        else if (player.IsGroundDetected || rb.velocity.y == 0)
        {
            Debug.Log($"wall state out 2 {player.IsGroundDetected}");
            stateMachine.ChangeState(player.playerIdleState);
        }
        else if (isJump)
        {
            Debug.Log("wall state out 3");
            stateMachine.ChangeState(player.playerWallJumpState);
        }
    }
}
