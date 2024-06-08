using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.setVelocity(rb.velocity.x, jumpHight);
        Debug.Log("enter jump");
        // Debug.Log($"jump{rb.velocity}");
        
        // Debug.Log($"jump {anim.GetBool("Jump")}");
        // Debug.Log($"jump {anim.GetBool(animBoolName)}");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("in jump exit");
    }

    public override void Update()
    {   
        Debug.Log("jump");
        base.Update();
        Debug.Log("in jump update");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
