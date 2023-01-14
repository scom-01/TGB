using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class CollisionSenses : CoreComponent
    {
        private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
        private Movement movement;
        public BoxCollider2D BC2D { get; private set; }

        #region Check Transforms

        public Transform GroundCheck { 
            get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name); 
            private set => groundCheck = value; 
        }
        public Transform WallCheck { 
            get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.parent.name); 
            private set => wallCheck = value; 
        }
        public Transform LedgeCheck { 
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheck, core.transform.parent.name);
            private set => ledgeCheck = value; 
        }

        public float GroundCheckRadius { get => core.Unit.UnitData.groundCheckRadius; set => core.Unit.UnitData.groundCheckRadius = value; }
        public float WallCheckDistance { get => core.Unit.UnitData.wallCheckDistance; set => core.Unit.UnitData.wallCheckDistance = value; }

        public LayerMask WhatIsGround { get => core.Unit.UnitData.whatIsGround; set => core.Unit.UnitData.whatIsGround = value; }

        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected Transform wallCheck;
        [SerializeField] protected Transform ledgeCheck;
        #endregion
        protected override void Awake()
        {
            base.Awake();
            BC2D = GetComponentInParent<BoxCollider2D>();
        }

        public bool CheckIfGrounded
        {
            get => Physics2D.OverlapBox(groundCheck.position, new Vector2(BC2D.bounds.size.x * 0.95f, BC2D.bounds.size.y * GroundCheckRadius), 0f, WhatIsGround);
            //get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        }

        public bool CheckIfTouchingWall
        {
            //Debug.DrawRay(wallCheck.position, Vector2.right * core.Movement.FancingDirection * wallCheckDistance, Color.green);
            get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FancingDirection, WallCheckDistance, WhatIsGround);
        }

        public bool CheckIfTouchingLedge
        {
            get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FancingDirection, WallCheckDistance, WhatIsGround);
        }

        public bool CheckIfTouchingWallBack
        {
            //Debug.DrawRay(wallCheck.position, Vector2.right * -core.Movement.FancingDirection * wallCheckDistance, Color.red);
            get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FancingDirection, WallCheckDistance, WhatIsGround);
        }
    }
}