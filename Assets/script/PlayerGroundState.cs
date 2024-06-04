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
        if (spaceDown && player.IsGroundDetected())
        {   
            stateMachine.ChangeState(player.playerJumpState);
        }
    }
}
