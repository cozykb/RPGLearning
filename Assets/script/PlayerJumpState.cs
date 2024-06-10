using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{   
    private bool setJump;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        setJump = true;
        // Debug.Log($"jump{rb.velocity}");
        
        // Debug.Log($"jump {anim.GetBool("Jump")}");
        // Debug.Log($"jump {anim.GetBool(animBoolName)}");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {   
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (setJump)
            player.fixSetVelocity(rb.velocity.x, jumpHight, 10, jumpHight);
        setJump = false;
    }
}
