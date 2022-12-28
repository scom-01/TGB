using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionSenses : EnemyCoreComponent
{
    public BoxCollider2D BC2D { get; private set; }

    #region Check Transforms

    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }

    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }

    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    public LayerMask WhatIsPlayer { get => whatIsPlayer; set => whatIsPlayer = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        BC2D = GetComponentInParent<BoxCollider2D>();
    }

    public bool CheckIfGrounded
    {
        get => Physics2D.OverlapBox(groundCheck.position, new Vector2(BC2D.bounds.size.x * 0.95f, BC2D.bounds.size.y * groundCheckRadius), 0f, whatIsGround);
        //get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool CheckIfTouchingWall
    {
        //Debug.DrawRay(wallCheck.position, Vector2.right * core.Movement.FancingDirection * wallCheckDistance, Color.green);
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FancingDirection, wallCheckDistance, whatIsGround);
    }

    public bool CheckIfTouchingLedge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FancingDirection, wallCheckDistance, whatIsGround);
    }

    public bool CheckIfTouchingWallBack
    {
        //Debug.DrawRay(wallCheck.position, Vector2.right * -core.Movement.FancingDirection * wallCheckDistance, Color.red);
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FancingDirection, wallCheckDistance, whatIsGround);
    }

    public bool CheckIfTouching
    {
        get => Physics2D.OverlapBox(transform.position + new Vector3((BC2D.offset.x + 0.1f) * core.Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x, BC2D.bounds.size.y * 0.95f), 0f, whatIsGround);
    }

    public bool CheckIfCliff
    {
        get => Physics2D.Raycast(groundCheck.position + new Vector3(BC2D.offset.x + BC2D.size.x / 2, 0, 0) * core.Movement.FancingDirection, Vector2.down, 0.2f, whatIsGround);
    }

    public bool PlayerDectected
    {
        get => Physics2D.Raycast(transform.position, Vector2.right * core.Movement.FancingDirection, core.GetComponentInParent<Enemy>().EnemyData.playerDetectedDistance, whatIsPlayer);
    }
}
