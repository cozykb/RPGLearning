using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState 
//PlayerState用来管理单个state的进出以及循环更新
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    #region playerInterface
    public readonly string animBoolName;
    protected float moveSpeed;
    protected float jumpHight;
    protected float dashSpeed;
    protected float faceDirection;
    // protected float airMoveSpeed;
    protected float yVelocity;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected float xInput;
    protected float StateTimer;
    protected bool spaceDown;
    protected bool MouseClick;
    protected bool TriggerCalled;
    protected static bool isJump;
    protected static float dashCD;
    protected static float dashDirection;
    protected static float dashDuration;
    private float dashUsageTime;
    #endregion playerInterface
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)  
    //这里接受Player的原因是需要从Player中读取类似于位置信息等内容
    //这里接受PlayerStateMachine是为了状态变更
    {
        this.player = _player; 
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    private void Awake()
    //这个用来初始化一些变量
    {
        anim = player.anim;
        rb = player.rb;
        moveSpeed = player.moveSpeed;
        jumpHight = player.jumpHight;
        dashSpeed = player.dashSpeed;
        faceDirection = player.faceDirection;
        dashCD = player.dashCD;
        dashDuration = player.dashDuration;
        // airMoveSpeed = player.airMoveSpeed;
    }
    public virtual void Enter()
    //管理state进入
    {   
        Debug.Log($"enter {animBoolName}");
        this.Awake();
        anim.SetBool(animBoolName, true);
        TriggerCalled = false;
        
    }

    public virtual void Update()
    //管理state循环更新
    {   

        xInput = Input.GetAxis("Horizontal");
        spaceDown = Input.GetKeyDown(KeyCode.Space);
        // 当通过fix update在单个state内更新状态时不能使用=
        // 而是需要|=或等于，在fix update内将变量转化为false
        // 以确保前后的正确同步
        StateTimer -= Time.deltaTime;
        isJump |= spaceDown;
        MouseClick = Input.GetKeyDown(KeyCode.Mouse0);
        GetDashInput();

        if(player.IsAttachToWall && !player.IsVelocityZero(rb.velocity.y) && animBoolName != "WallSlide" && xInput * player.WallAttachedDirection > 0)
        {   
            stateMachine.ChangeState(player.playerWallState);
        }

    }

    public virtual void Exit()
    //管理state跳出
    {   
        Debug.Log($"exit {animBoolName}");
        anim.SetBool(animBoolName, false);
    }

    public virtual void FixedUpdate() 
    {   
        yVelocity = rb.velocity.y;
    }

    private void GetDashInput()
    {   
        dashUsageTime -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTime <= 0)
        {
            stateMachine.ChangeState(player.playerDashState);
            dashUsageTime = dashCD;
        }

    }

    public virtual void AnimationFinishTrigger()
    {
        TriggerCalled = true;
    }


}
