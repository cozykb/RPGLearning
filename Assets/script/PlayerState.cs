using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState 
//PlayerState用来管理单个state的进出以及循环更新
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    private readonly string animBoolName;
    protected float moveSpeed;
    protected float jumpHight;
    // protected float airMoveSpeed;
    protected float yVelocity;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected float xInput;
    protected bool spaceDown;

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
        // airMoveSpeed = player.airMoveSpeed;
    }
    public virtual void Enter()
    //管理state进入
    {   

        this.Awake();
        anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    //管理state循环更新
    {
        xInput = Input.GetAxis("Horizontal");
        spaceDown = Input.GetKeyDown(KeyCode.Space);
        yVelocity = rb.velocity.y;
    }

    public virtual void Exit()
    //管理state跳出
    {
        anim.SetBool(animBoolName, false);
    }
}
