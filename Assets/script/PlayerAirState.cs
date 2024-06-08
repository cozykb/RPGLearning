using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
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
        if (!player.IsGroundDetected)
            anim.SetFloat("yVelocity", yVelocity);
        else
        {
            Debug.Log("transform to Idle");
            stateMachine.ChangeState(player.playerIdleState);
        }
        
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.setVelocity(xInput * moveSpeed, rb.velocity.y);
    }

}
