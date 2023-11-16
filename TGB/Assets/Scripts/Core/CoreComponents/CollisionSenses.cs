using UnityEngine;

namespace TGB.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
        protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
        protected Movement movement;

        public CapsuleCollider2D CC2D
        {
            get
            {
                if (cc2d == null)
                {
                    cc2d = GetComponentInParent<CapsuleCollider2D>();
                }
                return cc2d;
            }
            private set
            {
                cc2d = value;
            }
        }

        private CapsuleCollider2D cc2d;

        #region Check Transforms

        public Vector3 HeaderCenterPos
        {
            get
            {
                return new Vector3(core.Unit.transform.position.x + CC2D.offset.x, core.Unit.transform.position.y + CC2D.offset.y + CC2D.size.y / 2, 0);
            }
        }
        public Vector3 GroundCenterPos
        {
            get
            {
                return new Vector3(core.Unit.transform.position.x + CC2D.offset.x, core.Unit.transform.position.y + CC2D.offset.y - CC2D.size.y / 2, 0);
            }
        }

        public Vector3 WallCheck
        {
            get
            {
                return new Vector3(core.Unit.transform.position.x + CC2D.offset.x, core.Unit.transform.position.y + CC2D.offset.y - (CC2D.size.y / 2) + 0.7f, 0);
            }
            //get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name); 
            //private set => wallCheck = value; 
        }

        public float GroundCheckRadius { get => core.Unit.UnitData.groundCheckRadius; }
        public float WallCheckDistance { get => core.Unit.UnitData.wallCheckDistance; }

        public LayerMask WhatIsGround { get => core.Unit.UnitData.LayerMaskSO.WhatIsGround; }
        public LayerMask WhatIsWall { get => core.Unit.UnitData.LayerMaskSO.WhatIsWall; }
        public LayerMask WhatIsPlatform { get => core.Unit.UnitData.LayerMaskSO.WhatIsPlatform; }

        #endregion
        protected override void Awake()
        {
            base.Awake();
        }

        public bool CheckIfPlatform
        {
            get
            {
                return Physics2D.Raycast(GroundCenterPos + Vector3.down * (GroundCheckRadius) * 1 / 2, Vector3.up, (GroundCheckRadius) * 1 / 2, WhatIsPlatform);
                //return Physics2D.OverlapBox(GroundCenterPos + Vector3.down * (GroundCheckRadius) * 1 / 2, new Vector2(CC2D.bounds.size.x * 0.65f, CC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsPlatform);
            }
        }

        public bool CheckIfGrounded
        {
            get
            {
                return Physics2D.Raycast(GroundCenterPos + Vector3.down * (GroundCheckRadius) * 1 / 2, Vector3.up, (GroundCheckRadius) * 1 / 2, WhatIsGround);
                //return Physics2D.OverlapBox(GroundCenterPos + Vector3.down * (GroundCheckRadius) * 1 / 2, new Vector2(CC2D.bounds.size.x * 0.65f, CC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsGround);
            }
            //get => Physics2D.OverlapCircle(GroundCenterPos, groundCheckRadius, whatIsGround);
        }
        public bool CheckIfAirGrounded
        {
            get
            {
                return Physics2D.Raycast(GroundCenterPos + Vector3.down * (GroundCheckRadius), Vector3.up, (GroundCheckRadius) * 1 / 2, WhatIsGround);
                //return Physics2D.OverlapBox(GroundCenterPos + Vector3.down * (GroundCheckRadius), new Vector2(CC2D.bounds.size.x * 0.65f, CC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsGround);
            }
            //get => Physics2D.OverlapCircle(GroundCenterPos, groundCheckRadius, whatIsGround);
        }
        public bool CheckIfPlatformGrounded
        {
            get
            {
                return Physics2D.Raycast(GroundCenterPos + Vector3.down * (GroundCheckRadius), Vector3.up, (GroundCheckRadius) * 1 / 2, WhatIsPlatform);
                //return Physics2D.OverlapBox(GroundCenterPos + Vector3.down * (GroundCheckRadius), new Vector2(CC2D.bounds.size.x * 0.65f, CC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsPlatform);
            }
            //get => Physics2D.OverlapCircle(GroundCenterPos, groundCheckRadius, whatIsGround);
        }

        public bool CheckIfTouchingWall
        {
            //Debug.DrawRay(wallCheck.position, Vector2.right * core.Movement.FancingDirection * wallCheckDistance, Color.green);
            get => Physics2D.Raycast(WallCheck, Vector2.right * Movement.FancingDirection, CC2D.size.x / 2 + WallCheckDistance, WhatIsWall);
        }

        public bool CheckIfTouchingWallBack
        {
            //Debug.DrawRay(wallCheck.position, Vector2.right * -core.Movement.FancingDirection * wallCheckDistance, Color.red);
            get => Physics2D.Raycast(WallCheck, Vector2.right * -Movement.FancingDirection, CC2D.size.x / 2 + WallCheckDistance, WhatIsWall);
        }

        public bool CheckIfStayGrounded
        {
            get
            {
                return Physics2D.OverlapBox(transform.position + new Vector3((CC2D.offset.x + 0.1f) * Movement.FancingDirection, CC2D.offset.y), new Vector2(CC2D.bounds.size.x, CC2D.bounds.size.y * 0.65f), 0f, WhatIsGround) ||
                    Physics2D.OverlapBox(transform.position + new Vector3((CC2D.offset.x + 0.1f) * Movement.FancingDirection, CC2D.offset.y), new Vector2(CC2D.bounds.size.x, CC2D.bounds.size.y * 0.65f), 0f, WhatIsPlatform);
            }
        }

        public bool CheckIfCliff
        {
            get => Physics2D.Raycast(GroundCenterPos + new Vector3((CC2D.offset.x + 1) + CC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down, 0.5f, WhatIsGround) ||
                Physics2D.Raycast(GroundCenterPos + new Vector3((CC2D.offset.x + 1) + CC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down, 0.5f, WhatIsPlatform);
        }

        public bool CheckIfCliffBack
        {
            get => Physics2D.Raycast(GroundCenterPos + new Vector3((CC2D.offset.x + 1) + CC2D.size.x / 2, 0, 0) * -Movement.FancingDirection, Vector2.down, 0.5f, WhatIsGround) ||
                Physics2D.Raycast(GroundCenterPos + new Vector3((CC2D.offset.x + 1) + CC2D.size.x / 2, 0, 0) * -Movement.FancingDirection, Vector2.down, 0.5f, WhatIsPlatform);
        }

        public bool UnitDectected
        {
            get
            {
                RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right * Movement.FancingDirection, core.Unit.UnitData.UnitDetectedDistance, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);
                RaycastHit2D ray2 = Physics2D.Raycast(transform.position, Vector2.right * Movement.FancingDirection, core.Unit.UnitData.UnitDetectedDistance, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);
                RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right * Movement.FancingDirection, core.Unit.UnitData.UnitDetectedDistance, core.Unit.UnitData.LayerMaskSO.WhatIsEnemyUnit);
                return (ray1 || ray2 || ray3);
            }
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            if (CC2D == null)
                return;

            //Gizmos.DrawWireCube(transform.position + new Vector3((CC2D.offset.x) * Movement.FancingDirection, CC2D.offset.y - (CAPC2D.radius/2)), new Vector2(CC2D.bounds.size.x, (CC2D.bounds.size.y + CAPC2D.radius) * 0.95f));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(GroundCenterPos, GroundCenterPos + Vector3.down * (GroundCheckRadius));
            //CheckIfPlatformGrounded
            //Gizmos.DrawWireCube(GroundCenterPos + Vector3.down * (GroundCheckRadius),new Vector2(CC2D.bounds.size.x * 0.65f, GroundCheckRadius));
            Gizmos.color = Color.red;
            //CheckIfPlatform
            Gizmos.DrawLine(GroundCenterPos + Vector3.down * (GroundCheckRadius) / 2, GroundCenterPos + Vector3.down * (GroundCheckRadius));
            //Gizmos.DrawWireCube(GroundCenterPos + Vector3.down * (GroundCheckRadius) * 1 / 2,
            //    new Vector2(CC2D.bounds.size.x * 0.65f, GroundCheckRadius));


            //Gizmos.DrawLine(GroundCenterPos, GroundCenterPos + Vector3.down);

            //Gizmos.color = Color.blue;
            ////CheckIfTouchingWallBack
            //Gizmos.DrawLine(WallCheck, WallCheck + Vector3.right * -Movement.FancingDirection * (WallCheckDistance + CC2D.bounds.size.x / 2)); ;

            //Gizmos.color = Color.red;
            ////CheckIfTouchingWall
            //Gizmos.DrawLine(WallCheck, WallCheck + Vector3.right * Movement.FancingDirection * (WallCheckDistance + CC2D.bounds.size.x / 2));
        }
    }
}