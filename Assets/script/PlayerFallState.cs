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

        if (isJump && player.OutGRoundTimer < 50)
        {
            Debug.Log("fall to jump");
            stateMachine.ChangeState(player.playerJumpState);
            player.LockOutGRoundTimer();
        }
        if (rb.velocity.y == 0 || player.IsGroundDetected)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
