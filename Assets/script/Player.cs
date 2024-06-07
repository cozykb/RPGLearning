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

    #region states
    public PlayerState playerIdleState { get; private set; }
    public PlayerState playerMoveState { get; private set; }
    public PlayerState playerJumpState { get; private set; }
    public PlayerState playerFallState { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }
    #endregion states

    #region variables
    private float faceDirection = 1;
    private bool isRight = true;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    #endregion variables

    #region collision
    // [Header("Collision check")]
    // [SerializeField] private Transform groundCheck;
    // [SerializeField] private float groundCheckDistance;
    // [SerializeField] private Transform wallCheck;
    // [SerializeField] private float wallCheckDistance;
    // [SerializeField] private LayerMask layerMask;

    #endregion collision

    private void Awake()
    //初始化函数，在游戏开始时系统自动调用。一般用来创建变量之类的东西。
    {
        stateMachine = new PlayerStateMachine();
        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");
        playerMoveState = new PlayerMoveState(this, stateMachine, "Move");
        playerJumpState = new PlayerJumpState(this, stateMachine, "Jump");
        playerFallState = new PlayerFallState(this, stateMachine, "Fall");
    }


    private void Start()
    //初始化函数，在所有Awake函数运行完之后（一般是这样，但不一定），在所有Update函数前系统自动条用。一般用来给变量赋值。
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        stateMachine.Initialize(playerIdleState);
    }

    // Update is called once per frame
    private void Update()
    //这个地方利用stateMachine.currentState进行update以适应不同状态的切换
    {   
        stateMachine.currentState.Update();
    }

    private void Flip(float _x)
    {
        if (_x < 0 && isRight)
        {   
            faceDirection *= -1;
            isRight = !isRight;
            transform.Rotate(0, 180, 0);
        }
        else if (_x > 0 && !isRight)
        {
            faceDirection *= -1;
            isRight = !isRight;
            transform.Rotate(0, 180, 0);
        }
    }
    public void setVelocity(float _xVelocity, float _yVelocity)
    //因为最终velocity的变更对象为player所以这个方法只能定义在player下
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        Flip(_xVelocity);
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

    // public RaycastHit2D IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.right * faceDirection, groundCheckDistance, layerMask );
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color=Color.red;
    //     Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x + groundCheckDistance * faceDirection, groundCheck.position.y));
    // }
}
