using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{   
    protected float timer;
    private float duration;
    
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        duration = dashDuration;
        dashDirection = Input.GetAxisRaw("Horizontal");
        if (dashDirection == 0)
        {
            dashDirection = faceDirection;
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("exit dash");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (duration > 0)
        {  
            Debug.Log($"{dashSpeed}");
            player.fixSetVelocity(dashSpeed * dashDirection, 0, 10, dashSpeed);
    
        }
        duration -= Time.fixedDeltaTime; 
    }

    public override void Update()
    {
        base.Update();
        if (duration <= 0)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
    }


}
