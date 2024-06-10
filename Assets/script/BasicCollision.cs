using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BasicCollision : MonoBehaviour
{
    // Start is called before the first frame update
    #region variables
    public int faceDirection{ get; private set; } = 1;
    private bool isRight = true;
    public Rigidbody2D rb { get; private set; }
    private CapsuleCollider2D myCollider;
    public Animator anim { get; private set; }
    #endregion variables

    #region collision
    // [SerializeField] private LayerMask layerMask;
    // [Header("Collision check")]
    // [SerializeField] private Transform groundCheck;
    // [SerializeField] private float groundCheckDistance;
    // [SerializeField] private Transform wallCheck;
    // [SerializeField] private float wallCheckDistance;

    #endregion collision
    public bool IsGroundDetected { get; private set; } 
    public bool IsGroundStay { get; private set; } 
    public bool IsAttachToWall {get; private set; }
    public int WallAttachedDirection {get; private set; }
    public Vector2 CollisionDirection {get; private set;}

    protected virtual void Start()
    //初始化函数，在所有Awake函数运行完之后（一般是这样，但不一定），在所有Update函数前系统自动条用。一般用来给变量赋值。
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }
    protected virtual void Update()
    {
        IsGroundDetected = false;
        IsAttachToWall = false;
    }
    protected virtual void OnCollisionEnter2D(Collision2D _collision)
    {
        GetCollisionDirection(_collision);
        GroundDetector(CollisionDirection);
        WallDetector(CollisionDirection);
        Debug.Log($"Enter ground : {IsGroundDetected}");
        // Debug.DrawLine(transform.position, new Vector3(transform.position.x + CollisionDirection.x, transform.position.y + CollisionDirection.y), Color.red, 0.1f);
    }
    protected virtual void OnCollisionExit2D(Collision2D _collision) 
    {
        GetCollisionDirection(_collision);
        GroundDetector(CollisionDirection);
        WallDetector(CollisionDirection);
        Debug.Log($"Exit ground : {IsGroundDetected}");
        // Debug.Log($"Exit :{CollisionDirection}");
    }
    protected virtual void OnCollisionStay2D(Collision2D _collision) 
    //性能角度考虑，这个部分可以不检测地面
    {
        GetCollisionDirection(_collision, Color.blue);
        // Debug.Log($"CD 2 : {CollisionDirection}");
        GroundDetector(CollisionDirection);
        WallDetector(CollisionDirection);
        Debug.Log($"stay ground : {IsGroundDetected}");
        // Debug.DrawLine(transform.position, new Vector3(transform.position.x + CollisionDirection.x, transform.position.y + CollisionDirection.y), Color.green, 0.1f);
    }

    protected virtual void GetCollisionDirection(Collision2D _collision, Color? _color = null)
    {   
        //靠墙时的检测是错误的
        // if (_color == null)
        //     _color = Color.red;
        CollisionDirection = Vector2.zero;
        // Debug.Log($"layer : {_collision.gameObject.layer}");
        // Debug.Log($"getmask : {LayerMask.GetMask("Ground")}");
        // Debug.Log($"name to layer : {LayerMask.NameToLayer("Ground")}");
        //这里的layer是一个index
        if (_collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            for (int i = 0; i < _collision.contactCount; i++)
            {   
                // var CollisionDirectionPart = _collision.GetContact(i).normal;
                // Debug.Log($"CD 4 : {i} ,{CollisionDirectionPart}");
                CollisionDirection += _collision.GetContact(i).normal;
                // Debug.Log($"CD 5 : {i} ,{ transform.position + new Vector3(CollisionDirectionPart.x, CollisionDirectionPart.y)}");
                // Debug.DrawLine(transform.position, transform.position + new Vector3(CollisionDirectionPart.x, CollisionDirectionPart.y), (Color)_color, 1);
            }
        }
        // Debug.Log($"CD 3 : {CollisionDirection}");
        CollisionDirection.Normalize();
        // Debug.Log($"CD : {CollisionDirection}");
        Debug.Log($"CD count : {_collision.contactCount}");
    }

    protected virtual void GroundDetector(Vector2 _collisionDirection)
    {   
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
    public void setVelocity(float _xVelocity, float _yVelocity, float maxAcceleration, float maxSpeed)
    //因为最终velocity的变更对象为player所以这个方法只能定义在player下
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        var acceleration = maxAcceleration * Time.fixedDeltaTime;
        var targetSpeed = new Vector2(_xVelocity, _yVelocity) * maxSpeed;
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, targetSpeed.x, acceleration),
                                    Mathf.MoveTowards(rb.velocity.y, targetSpeed.y, acceleration));
        Flip(_xVelocity);
    }
    public void setGravity(float _gravity)
    {
        rb.gravityScale = _gravity;
    }

    public void fixSetVelocity(float _xVelocity, float _yVelocity, float maxAcceleration, float maxSpeed)
    {
        setVelocity(_xVelocity, _yVelocity , maxAcceleration, maxSpeed);
        Flip(_xVelocity);
        EdgeFix();
    }

    private Collider2D Hit(Vector2 _position)
    {
        return Physics2D.OverlapBox(_position, myCollider.bounds.size, 0, LayerMask.GetMask("Ground"));
    }
    private void EdgeFix()
    {
        //获取速度方向内预期的移动
        Vector2 moveMent = rb.velocity * Time.fixedDeltaTime;
        var futurePosition = transform.position + new Vector3(moveMent.x, moveMent.y);
        Collider2D hit = Hit(futurePosition);
        if (hit != null)
        {
            // Debug.Log($"hit pos : {hit.transform.position}"); 
            //这东西是被撞击到的物体的transform
            // Debug.Log($"cur pos : {transform.position}");
            var fixPos = (transform.position - hit.transform.position).normalized;
            Vector3 tryPos = transform.position + fixPos * moveMent.magnitude + new Vector3(moveMent.x, moveMent.y);
            Debug.Log("try fix");
            if (!IsGroundDetected && !Hit(tryPos))
            {   
                Debug.Log("fix");
                transform.position = transform.position + fixPos * moveMent.magnitude;
            }
        }
    
    }

}
