using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    // Start is called before the first frame update
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("enter fall state");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("exit fall state");
    }

    public override void Update()
    {
        base.Update();
        Debug.Log("in fall state update");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Debug.Log("in fall state fix update");
    }
}
