using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(xInput == 0)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
        
    }
    public override void FixedUpdate()
    {
        if (!player.IsBusy)
            player.setVelocity(xInput * moveSpeed, rb.velocity.y, 50);

    }
}
