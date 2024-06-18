using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{   
    protected bool updateAvailable = false;
    protected int timer = 0;
    protected bool setJump;
    protected int timerUpdate = 0;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        setJump = true;
        updateAvailable = false;
        // Debug.Log($"jump{rb.velocity}");
        
        // Debug.Log($"jump {anim.GetBool("Jump")}");
        // Debug.Log($"jump {anim.GetBool(animBoolName)}");
    }

    public override void Update()
    {        
        timerUpdate += 1;
        if (updateAvailable)
        {
            base.Update();
            if (!player.IsGroundDetected || rb.velocity.y != 0)
            {
                anim.SetFloat("yVelocity", rb.velocity.y);
            }
            // else if (!player.IsGroundDetected && rb.velocity.y < 0)
            // {
            //     anim.SetFloat("yVelocity", rb.velocity.y);
            // }
            else
            {
                // Debug.Log($"jump transform to Idle, {player.IsGroundDetected}, {rb.velocity.y}, {isJump}, {updateAvailable}, fix {timer}, update {timerUpdate}");
                stateMachine.ChangeState(player.playerIdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        isJump = false;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        timer += 1;
        if (setJump)
            player.dirctSetVelocity(rb.velocity.x, jumpHight);
        setJump = false;
        if (!player.IsVelocityZero(rb.velocity.y)  && !player.IsGroundDetected)
            updateAvailable = true;
        
        player.fixSetVelocity(xInput * moveSpeed * 0.8f, rb.velocity.y, 50);
    }
}
