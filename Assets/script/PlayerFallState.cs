using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    // Start is called before the first frame update
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (isJump && player.outGRoundTimer < 50)
        {
            stateMachine.ChangeState(player.playerJumpState);
            player.LockOutGRoundTimer();
            isJump = false;
        }
        if (rb.velocity.y == 0)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
