using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("enter move state");
    }
    public override void Exit()
    {
        base.Exit();
        Debug.Log("exit move state");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("in move state update");
        if(xInput == 0)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
        
    }
    public override void FixedUpdate()
    {
        Debug.Log("in move state fix update");
        player.setVelocity(xInput * moveSpeed, rb.velocity.y);

    }
}
