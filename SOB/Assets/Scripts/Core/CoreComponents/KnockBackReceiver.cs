using UnityEngine;

namespace SOB.CoreSystem
{
    public class KnockBackReceiver : CoreComponent, IKnockBackable
    {
        [SerializeField] private float maxKnockBackTime = 0.2f;

        private bool isKnockBackActive;
        private float knockBackStartTime;

        private CoreComp<Movement> movement;
        private CoreComp<CollisionSenses> collisionSenses;
        private CoreComp<Death> death;
        private CoreComp<DamageReceiver> damageReceiver;

        public override void LogicUpdate()
        {
            CheckKnockBack();
        }

        
        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            
            if(death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }

            //CC기 면역
            if(core.Unit.isCCimmunity)
            {
                return;
            }

            movement.Comp?.SetVelocity(strength, angle, direction);
            movement.Comp.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }
        public void KnockBack(Vector2 angle, float strength)
        {
            
            if(death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }
            movement.Comp?.SetVelocity(strength, angle, movement.Comp.FancingDirection);
            movement.Comp.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }
        public void TrapKnockBack(Vector2 angle, float strength)
        {
            
            if(death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }
            if(damageReceiver.Comp.isTouch)
            {
                return;
            }
            movement.Comp?.SetVelocity(strength, angle, movement.Comp.FancingDirection);
            movement.Comp.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }
        private void CheckKnockBack()
        {
            if (isKnockBackActive
                && ((movement.Comp?.CurrentVelocity.y <= 0.01f && collisionSenses.Comp.GroundCheck)
                    || Time.time >= knockBackStartTime + maxKnockBackTime)
               )
            {
                isKnockBackActive = false;
                movement.Comp.CanSetVelocity = true;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            movement = new CoreComp<Movement>(core);
            collisionSenses = new CoreComp<CollisionSenses>(core);
            death = new CoreComp<Death>(core);
            damageReceiver = new CoreComp<DamageReceiver>(core);
        }
    }
}