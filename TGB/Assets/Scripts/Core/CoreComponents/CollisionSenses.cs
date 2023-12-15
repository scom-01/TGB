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
        public Rigidbody2D RB
        {
            get
            {
                if (rb == null)
                {
                    rb = GetComponentInParent<Rigidbody2D>();
                }
                return rb;
            }
            private set
            {
                rb = value;
            }
        }

        private Rigidbody2D rb;

        #region Check Transforms

        /// <summary>
        /// 유닛 정가운데 위치
        /// </summary>
        public Vector3 UnitCenterPos
        {
            get
            {
                return new Vector3(core.Unit.transform.position.x + CC2D.offset.x, core.Unit.transform.position.y + CC2D.offset.y, 0);
            }
        }

        public Vector3 UnitCenterFront => new Vector3(UnitCenterPos.x + (CC2D.size.x * 0.5f * Movement.FancingDirection), UnitCenterPos.y);
        public Vector3 UnitCenterBack => new Vector3(UnitCenterPos.x + (CC2D.size.x * 0.5f * -Movement.FancingDirection), UnitCenterPos.y);

        /// <summary>
        /// 머리높이의 가운데 위치
        /// </summary>
        public Vector3 HeaderCenterPos
        {
            get
            {
                return new Vector3(core.Unit.transform.position.x + CC2D.offset.x, core.Unit.transform.position.y + CC2D.offset.y + CC2D.size.y / 2, 0);
            }
        }

        /// <summary>
        /// 바닥높이의 가운데 위치
        /// </summary>
        public Vector3 GroundCenterPos
        {
            get
            {
                return new Vector3(core.Unit.transform.position.x + CC2D.offset.x, core.Unit.transform.position.y + CC2D.offset.y - CC2D.size.y / 2, 0);
            }
        }

        public Vector3 GroundFront => new Vector3(GroundCenterPos.x + (CC2D.size.x / 2 * Movement.FancingDirection), GroundCenterPos.y, 0);
        public Vector3 GroundBack => new Vector3(GroundCenterPos.x + (CC2D.size.x / 2 * -Movement.FancingDirection), GroundCenterPos.y, 0);

        /// <summary>
        /// 신체높이의 70% 지점
        /// </summary>
        public Vector3 WallFrontPos
        {
            get
            {
                return new Vector3(UnitCenterPos.x + (CC2D.size.x / 2) * Movement.FancingDirection, UnitCenterPos.y + ((CC2D.size.y / 2) * 0.7f), 0);
            }
        }
        public Vector3 WallkBackPos
        {
            get
            {
                return new Vector3(UnitCenterPos.x + (CC2D.size.x / 2) * -Movement.FancingDirection, UnitCenterPos.y + ((CC2D.size.y / 2) * 0.7f), 0);
            }
        }

        public Vector3 CliffFront => GroundFront + (Vector3.right * WallCheckDistance * Movement.FancingDirection);

        public Vector3 CliffBack => GroundBack + (Vector3.right * WallCheckDistance * -Movement.FancingDirection);

        public float GroundCheckDistance { get => core.Unit.UnitData.groundCheckDistance; }
        public float WallCheckDistance { get => core.Unit.UnitData.wallCheckDistance; }

        public LayerMask WhatIsGround { get => core.Unit.UnitData.LayerMaskSO.WhatIsGround; }
        public LayerMask WhatIsWall { get => core.Unit.UnitData.LayerMaskSO.WhatIsWall; }
        public LayerMask WhatIsPlatform { get => core.Unit.UnitData.LayerMaskSO.WhatIsPlatform; }

        protected ContactFilter2D contactFilter_Ground;
        protected ContactFilter2D contactFilter_Platform;
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        #endregion
        protected override void Awake()
        {
            base.Awake();
            contactFilter_Ground.SetLayerMask(WhatIsGround);
            contactFilter_Platform.SetLayerMask(WhatIsPlatform);
            contactFilter_Ground.useLayerMask = true;
            contactFilter_Platform.useLayerMask = true;
        }

        public bool CheckIfPlatform
        {
            get
            {
                var count = Physics2D.Raycast(GroundCenterPos, Vector2.down, contactFilter_Platform, hitBuffer, GroundCheckDistance);
                if (count > 0)
                {
                    foreach (var hit in hitBuffer)
                    {
                        if (hit.rigidbody == null)
                            continue;

                        //hit의 기울기(양수면 hit의 y가 더 낮은 위치, 즉 GroundCenterPos가 hit.point보다 위에 있으면 양수)
                        if (hit.normal.y < 0.9f)
                            continue;

                        //hit의 포인트(Ray가 부딪힌 지점)
                        if (GroundCenterPos.y > hit.point.y)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool CheckIfGrounded
        {
            get
            {
                var count = Physics2D.Raycast(GroundCenterPos, Vector2.down, contactFilter_Ground, hitBuffer, GroundCheckDistance);
                if (count > 0)
                {
                    foreach (var hit in hitBuffer)
                    {
                        if (hit.rigidbody == null)
                            continue;

                        //hit의 기울기(양수면 hit의 y가 더 낮은 위치, 즉 GroundCenterPos가 hit.point보다 위에 있으면 양수)
                        if (hit.normal.y < 0.9f)
                            continue;

                        //hit의 포인트(Ray가 부딪힌 지점)
                        if (GroundCenterPos.y > hit.point.y)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool CheckIfTouchingWall
        {
            get => Physics2D.Raycast(WallFrontPos, Vector2.right * Movement.FancingDirection, WallCheckDistance, WhatIsWall);
        }

        public bool CheckIfTouchingWallBack
        {
            get => Physics2D.Raycast(WallkBackPos, Vector2.right * -Movement.FancingDirection, WallCheckDistance, WhatIsWall);
        }

        public bool CheckIfCliff
        {
            get => Physics2D.Raycast(CliffFront, Vector2.down, 0.5f, WhatIsGround) ||
                Physics2D.Raycast(CliffFront, Vector2.down, 0.5f, WhatIsPlatform);
        }

        public bool CheckIfCliffBack
        {
            get => Physics2D.Raycast(CliffBack, Vector2.down, 0.5f, WhatIsGround) ||
                Physics2D.Raycast(CliffBack, Vector2.down, 0.5f, WhatIsPlatform);
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

            //CheckCeiling
            Gizmos.DrawLine(HeaderCenterPos, HeaderCenterPos + Vector3.up * (GroundCheckDistance));

            //checkGround,Platform
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(GroundCenterPos, GroundCenterPos + Vector3.down * (GroundCheckDistance));

            Gizmos.color = Color.yellow;
            //CheckIfCliff
            Gizmos.DrawLine(CliffFront, CliffFront + Vector3.down * WallCheckDistance);

            Gizmos.color = Color.magenta;
            //CheckIfCliffBack
            Gizmos.DrawLine(CliffBack, CliffBack + Vector3.down * WallCheckDistance);

            Gizmos.color = Color.blue;
            //CheckIfTouchingWall
            Gizmos.DrawLine(WallFrontPos, WallFrontPos + Vector3.right * Movement.FancingDirection * (WallCheckDistance));

            Gizmos.color = Color.red;
            //CheckIfTouchingWallBack
            Gizmos.DrawLine(WallkBackPos, WallkBackPos + Vector3.right * -Movement.FancingDirection * (WallCheckDistance));
        }
    }
}