using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
//用来兼容所有地面动作，当调用其中update方法后离开地面状态转为其他非地面模式
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();
        if (spaceDown)
            Debug.Log($"jump timer : {player.outGRoundTimer}");
        if (isJump && (player.IsGroundDetected || player.IsAttachToWall))
        //这里会有一帧的IsGroundDetected=true被传入。
        {   
            player.SetIsGround(false);
            //解决被传入的一帧IsGroundDetected=true,但没解决掉
            stateMachine.ChangeState(player.playerJumpState);
            isJump = false;
        }
        else if (!player.IsGroundDetected && rb.velocity.y != 0)
        {   
            Debug.Log("jump to fall");
            stateMachine.ChangeState(player.playerFallState);
        }
    }

}
