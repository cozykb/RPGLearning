using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsGroundDetected { get; private set; } 
    public bool IsAttachToWall {get; private set; }
    public int WallAttachedDirection {get; private set; }
    public Vector2 CollisionDirection {get; private set;}

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
        Debug.Log($"CD : {CollisionDirection}");
    }

    protected virtual void GroundDetector(Vector2 _collisionDirection)
    {   
        IsGroundDetected = false;
        if (_collisionDirection.y > 0)
        {   
            Debug.Log("IsGroundDetected = true;");
            IsGroundDetected = true;
        }
        else
        {
            Debug.Log("IsGroundDetected = false;");
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
}
