using System.Threading;
using UnityEngine;

public class PlayerWallJumpState : PlayerJumpState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log($"wall jump ; is update available : {updateAvailable}; setJump {setJump}");
    }

    public override void Update()
    {   
        Debug.Log($"wall jump {player.IsAttachToWall}");
        base.Update();
    }
    public override void FixedUpdate()
    {
        Debug.Log($"wall jump ; is update available : {updateAvailable}; fix update");
        timer += 1;
        if (setJump)
        {
            player.dirctSetVelocity(-player.WallAttachedDirection * moveSpeed * 0.8f, jumpHight * 0.6f);
        }
        setJump = false;
        if ( !player.IsVelocityZero(rb.velocity.y) && !player.IsAttachToWall)
            updateAvailable = true;
        
        // player.fixSetVelocity(xInput * moveSpeed * 0.8f, rb.velocity.y, 50);
    }
    
}
