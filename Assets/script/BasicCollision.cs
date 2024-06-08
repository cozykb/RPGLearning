using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCollision : MonoBehaviour
{
    // Start is called before the first frame update
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
    public bool IsGroundDetected { get; private set; } 
    public bool IsAttachToWall {get; private set; }
    public int WallAttachedDirection {get; private set; }
    public Vector2 CollisionDirection {get; private set;}

    protected virtual void Start()
    //初始化函数，在所有Awake函数运行完之后（一般是这样，但不一定），在所有Update函数前系统自动条用。一般用来给变量赋值。
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D _collision)
    {
        GetCollisionDirection(_collision);
        GroundDetector(CollisionDirection);
        WallDetector(CollisionDirection);
        // Debug.DrawLine(transform.position, new Vector3(transform.position.x + CollisionDirection.x, transform.position.y + CollisionDirection.y), Color.red, 0.1f);
    }
    protected virtual void OnCollisionExit2D(Collision2D _collision) 
    {
        GetCollisionDirection(_collision);
        GroundDetector(CollisionDirection);
        WallDetector(CollisionDirection);
        // Debug.Log($"Exit :{CollisionDirection}");
    }
    protected virtual void OnCollisionStay2D(Collision2D _collision) 
    //性能角度考虑，这个部分可以不检测地面
    {
        GetCollisionDirection(_collision);
        // GroundDetector(CollisionDirection);
        WallDetector(CollisionDirection);
        // Debug.DrawLine(transform.position, new Vector3(transform.position.x + CollisionDirection.x, transform.position.y + CollisionDirection.y), Color.green, 0.1f);
    }

    protected virtual void GetCollisionDirection(Collision2D _collision)
    {   
        CollisionDirection = Vector2.zero;
        for (int i = 0; i < _collision.contactCount; i++)
        {
            CollisionDirection += _collision.GetContact(i).normal;
        }
        CollisionDirection.Normalize();
        // Debug.Log($"CD : {CollisionDirection}");
    }

    protected virtual void GroundDetector(Vector2 _collisionDirection)
    {   
        IsGroundDetected = false;
        if (_collisionDirection.y > 0)
        {   
            // Debug.Log("IsGroundDetected = true;");
            IsGroundDetected = true;
        }
        else
        {
            // Debug.Log("IsGroundDetected = false;");
            IsGroundDetected = false;
        }
    }
    protected virtual void WallDetector(Vector2 _collisionDirection)
    {
        if (_collisionDirection.x != 0)
        {
            IsAttachToWall = true;
            if (_collisionDirection.x > 0)
            {
                WallAttachedDirection = 1;
            }
            else
            {
                WallAttachedDirection = -1;
            }
        }
        else
        {
            IsAttachToWall = false;
        }
    }
    public void SetIsGround(bool _value)
    {
        IsGroundDetected = _value;
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
}
