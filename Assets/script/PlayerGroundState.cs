using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
//用来兼容所有地面动作，当调用其中update方法后离开地面状态转为其他非地面模式
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (spaceDown && player.IsGroundDetected)
        //这里会有一帧的IsGroundDetected=true被传入。
        {   
            player.SetIsGround(false);
            //解决被传入的一帧IsGroundDetected=true
            stateMachine.ChangeState(player.playerJumpState);
        }
        if (!player.IsGroundDetected)
        {
            stateMachine.ChangeState(player.playerFallState);
        }
    }
}
