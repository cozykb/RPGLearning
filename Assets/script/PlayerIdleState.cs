using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerIdleState : PlayerGroundState
{
    // Start is called before the first frame update
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    //根据状态的不同在这里为状态创建进入方案
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

        if(xInput != 0)
        {
            stateMachine.ChangeState(player.playerMoveState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.setVelocity(0,0);
    }
}
