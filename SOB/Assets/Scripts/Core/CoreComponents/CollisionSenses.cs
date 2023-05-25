using UnityEngine;

namespace SOB.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
        protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
        protected Movement movement;
        public BoxCollider2D BC2D 
        {
            get 
            { 
                if(bc2d == null)
                {
                    bc2d = GetComponentInParent<BoxCollider2D>();
                }
                return bc2d; 
            }
            private set
            {
                bc2d = value;
            }
        }

        private BoxCollider2D bc2d;

        #region Check Transforms

        public Transform GroundCheck { 
            get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name); 
            private set => groundCheck = value; 
        }
        public Transform WallCheck { 
            get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name); 
            private set => wallCheck = value; 
        }

        public float GroundCheckRadius { get => core.Unit.UnitData.groundCheckRadius; }
        public float WallCheckDistance { get => core.Unit.UnitData.wallCheckDistance; }

        public LayerMask WhatIsGround { get => core.Unit.UnitData.whatIsGround; }
        public LayerMask WhatIsWall { get => core.Unit.UnitData.whatIsWall; }
        public LayerMask WhatIsPlatform { get => core.Unit.UnitData.whatIsPlatform; }

        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected Transform wallCheck;
        #endregion
        protected override void Awake()
        {
            base.Awake();
        }

        public bool CheckIfPlatform
        {
            get => Physics2D.OverlapBox(groundCheck.position, new Vector2(BC2D.bounds.size.x * 0.95f, BC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsPlatform);
        }

        public bool CheckIfGrounded
        {
            get => Physics2D.OverlapBox(groundCheck.position, new Vector2(BC2D.bounds.size.x * 0.95f, BC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsGround);
            //get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }

        public bool CheckIfTouchingWall
        {
            //Debug.DrawRay(wallCheck.position, Vector2.right * core.Movement.FancingDirection * wallCheckDistance, Color.green);
            get => Physics2D.Raycast(wallCheck.position, Vector2.right * Movement.FancingDirection, BC2D.size.x / 2 + WallCheckDistance, WhatIsWall);
        }

        public bool CheckIfTouchingWallBack
        {
            //Debug.DrawRay(wallCheck.position, Vector2.right * -core.Movement.FancingDirection * wallCheckDistance, Color.red);
            get => Physics2D.Raycast(wallCheck.position, Vector2.right * -Movement.FancingDirection, BC2D.size.x / 2 + WallCheckDistance, WhatIsWall);
        }

        public bool CheckIfStayGrounded
        {
            get => Physics2D.OverlapBox(transform.position + new Vector3((BC2D.offset.x + 0.1f) * Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x, BC2D.bounds.size.y * 0.95f), 0f, WhatIsGround);
        }

        public bool CheckIfCliff
        {
            get => Physics2D.Raycast(groundCheck.position + new Vector3((BC2D.offset.x + 1) + BC2D.size.x / 2, 0, 0) * Movement.FancingDirection, Vector2.down, 0.5f, WhatIsGround);
        }

        public bool CheckIfCliffBack
        {
            get => Physics2D.Raycast(groundCheck.position + new Vector3((BC2D.offset.x + 1) + BC2D.size.x / 2, 0, 0) * -Movement.FancingDirection, Vector2.down, 0.5f, WhatIsGround);
        }

        public bool UnitDectected
        {
            get
            {
                RaycastHit2D ray1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.right * Movement.FancingDirection, core.Unit.UnitData.UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
                RaycastHit2D ray2 = Physics2D.Raycast(transform.position, Vector2.right * Movement.FancingDirection, core.Unit.UnitData.UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
                RaycastHit2D ray3 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.right * Movement.FancingDirection, core.Unit.UnitData.UnitDetectedDistance, core.Unit.UnitData.WhatIsEnemyUnit);
                return (ray1 || ray2 || ray3);
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            if (BC2D == null)
                return;
            Gizmos.DrawWireCube(transform.position + new Vector3((BC2D.offset.x) * Movement.FancingDirection, BC2D.offset.y), new Vector2(BC2D.bounds.size.x, BC2D.bounds.size.y * 0.95f));
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, new Vector2(BC2D.bounds.size.x * 0.95f, BC2D.bounds.size.y * GroundCheckRadius));
            Debug.DrawRay(groundCheck.position, Vector2.down * GroundCheckRadius *4, Color.red);

            //CheckIfTouchingWallBack
            Debug.DrawRay(wallCheck.position, Vector2.right * -Movement.FancingDirection * (WallCheckDistance + BC2D.bounds.size.x / 2), Color.red);

            //CheckIfTouchingWall
            Debug.DrawRay(wallCheck.position, Vector2.right * Movement.FancingDirection * (WallCheckDistance + BC2D.bounds.size.x / 2), Color.green);
        }
    }
}