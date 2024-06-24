using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerGroundState
{   
    private int ComboCounter;
    private float LastTimeAttacked;
    private float ComboTimeWindow = 2;
    private float Direction;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (ComboCounter > 2 || (Time.time - LastTimeAttacked >= ComboTimeWindow))
        {
            ComboCounter = 0;
        }
        player.BusyOccupancy();
        anim.SetInteger("ComboCounter", ComboCounter);
        player.setVelocity(0, 0, 300);
        float LocalXInput = Input.GetAxis("Horizontal");
        if ( LocalXInput * faceDirection < 0)
        {
            Direction = faceDirection * -1;
        }
        else
        {
            Direction = faceDirection;
        }
    }

    public override void Exit()
    {
        base.Exit();
        ComboCounter += 1;
        LastTimeAttacked = Time.time;
        player.StartCoroutine("BusyTime", .2f);

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.setVelocity(player.AttackMovement[ComboCounter].x * Direction, player.AttackMovement[ComboCounter].y, 5);
    }

    public override void Update()
    {
        base.Update();
        if (TriggerCalled)
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
    }
}
