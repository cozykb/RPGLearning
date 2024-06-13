using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState 
//PlayerState用来管理单个state的进出以及循环更新
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    #region playerInterface
    private readonly string animBoolName;
    protected float moveSpeed;
    protected float jumpHight;
    protected float dashSpeed;
    protected int faceDirection;
    // protected float airMoveSpeed;
    protected float yVelocity;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected float xInput;
    protected bool spaceDown;
    protected bool isJump;
    protected float dashCD;
    protected float dashDirection;
    protected float dashDuration;
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
    }

    public virtual void Update()
    //管理state循环更新
    {   

        xInput = Input.GetAxis("Horizontal");
        spaceDown = Input.GetKeyDown(KeyCode.Space);
        // 当通过fix update在单个state内更新状态时不能使用=
        // 而是需要|=或等于，在fix update内将变量转化为false
        // 以确保前后的正确同步
        isJump |= spaceDown;
        GetDashInput();
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
}
