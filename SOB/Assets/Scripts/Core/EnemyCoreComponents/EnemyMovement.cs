using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyCoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public int FancingDirection { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;



    protected override void Awake()
    {
        base.Awake();
        FancingDirection = 1;
        RB = GetComponentInParent<Rigidbody2D>();
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    public void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, Mathf.Clamp(angle.y * velocity, -3, 13));
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    //2D Filp
    public void Flip()
    {
        FancingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
