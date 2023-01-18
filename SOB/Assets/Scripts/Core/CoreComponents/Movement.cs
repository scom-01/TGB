using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class Movement : CoreComponent
    {
        public Rigidbody2D RB { get; private set; }

        public int FancingDirection { get; private set; }
        public bool CanSetVelocity { get; set; }
        public Vector2 CurrentVelocity { get; private set; }

        protected Vector2 workspace;

        protected override void Awake()
        {
            base.Awake();
            RB = GetComponentInParent<Rigidbody2D>();

            FancingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = RB.velocity;
        }

        #region Set Func
        public void SetVelocityZero()
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, Mathf.Clamp(angle.y * velocity, -3, 13));
            SetFinalVelocity();
        }
        public void SetVelocityX(float velocity)
        {
            workspace.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }
        public void SetVelocityY(float velocity)
        {
            workspace.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if(CanSetVelocity)
            {
                RB.velocity = workspace;
                CurrentVelocity = workspace;
            }
        }
        #endregion Set Func

        #region Flip
        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FancingDirection)
            {
                Flip();
            }
        }

        //2D Filp
        public void Flip()
        {
            FancingDirection *= -1;
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
        #endregion Flip
    }
}