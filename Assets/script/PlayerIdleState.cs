using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerIdleState : PlayerGroundState
{
    // Start is called before the first frame update
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
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
        player.setVelocity(0,0,100);
    }
}
