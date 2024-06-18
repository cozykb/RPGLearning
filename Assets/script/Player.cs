using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class Player : BasicCollision
{
    #region movement
    [Header("movement parameter")]
    public float moveSpeed;
    public float jumpHight;
    // public float airMoveSpeed;
    #endregion movement
    #region Dash
    [Header("Dash")]
    public float dashSpeed;
    public float dashCD;
    public float dashDuration;
    #endregion Dash
    #region states
    public PlayerState playerIdleState { get; private set; }
    public PlayerState playerMoveState { get; private set; }
    public PlayerState playerJumpState { get; private set; }
    public PlayerState playerFallState { get; private set; }
    public PlayerState playerDashState { get; private set; }
    public PlayerState playerWallState { get; private set; }
    public PlayerState playerWallJumpState { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    #endregion states
    #region other
    public int OutGRoundTimer{ get; private set; }
    #endregion other


    private void Awake()
    //初始化函数，在游戏开始时系统自动调用。一般用来创建变量之类的东西。
    {
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");
        playerMoveState = new PlayerMoveState(this, stateMachine, "Move");
        playerJumpState = new PlayerJumpState(this, stateMachine, "Jump");
        playerFallState = new PlayerFallState(this, stateMachine, "Fall");
        playerDashState = new PlayerDashState(this, stateMachine, "Dash");
        playerWallState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        playerWallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
    }
    protected override void Start() 
    {
        base.Start();
        stateMachine.Initialize(playerIdleState);
    }

    // Update is called once per frame
    protected override void Update()
    //这个地方利用stateMachine.currentState进行update以适应不同状态的切换
    {   
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
        OutGRoundTimer = IsGroundDetected ? 0 : OutGRoundTimer + 1;
    }


    protected override void OnCollisionEnter2D(Collision2D _collision)
    {
        base.OnCollisionEnter2D(_collision);
    }

    protected override void OnCollisionExit2D(Collision2D _collision)
    {
        base.OnCollisionExit2D(_collision);
    }

    protected override void OnCollisionStay2D(Collision2D _collision)
    {
        base.OnCollisionStay2D(_collision);
    }

    public void LockOutGRoundTimer()
    {
        OutGRoundTimer += 100;
    }

    // public RaycastHit2D IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.right * faceDirection, groundCheckDistance, layerMask );
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color=Color.red;
    //     Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x + groundCheckDistance * faceDirection, groundCheck.position.y));
    // }
}
