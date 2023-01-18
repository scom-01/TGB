using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB
{
    public class Monster1 : Enemy
    {
        public EnemyIdleState IdleState { get; private set; }
        public EnemyRunState RunState { get; private set; }
        public EnemyAttackState AttackState { get; private set; }
        public EnemyHitState HitState { get; private set; }
        public EnemyDeadState DeadState { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            IdleState = new EnemyIdleState(this, "idle");
            RunState = new EnemyRunState(this, "run");
            AttackState = new EnemyAttackState(this, "attack");
            HitState = new EnemyHitState(this, "hit");
            DeadState = new EnemyDeadState(this, "dead");
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate(); 
        }
        protected override void Start()
        {
            base.Start();
            FSM.Initialize(IdleState);
        }
    }
}
