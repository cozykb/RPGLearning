using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class PlayerAirState : PlayerState
{   
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDetected)
            anim.SetFloat("yVelocity", rb.velocity.y);
        else
        {
            Debug.Log("air transform to Idle");
            stateMachine.ChangeState(player.playerIdleState);
        }
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.fixSetVelocity(xInput * moveSpeed * 0.8f, rb.velocity.y, 50);
    }

}
