using SOB.CoreSystem;
using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class EffectSpawn : WeaponComponent<EffectSpawnData, ActionEffect>
    {

        private int currentEffectSpawnIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentEffectSpawnIndex = 0;
        }

        private void HandleEffectSpawn()
        {
            if (currentActionData != null)
            {
                CheckEffectAction(currentActionData);
            }

            currentEffectSpawnIndex++;
        }

        private void CheckEffectAction(ActionEffect actionParticle)
        {
            if (actionParticle == null)
                return;

            var currParticles = actionParticle.EffectParticles;
            if (currParticles.Length <= 0)
                return;


            if (currentEffectSpawnIndex >= currParticles.Length)
            {
                Debug.Log($"{weapon.name} Particle Prefabs length mismatch");
                return;
            }

            if (currParticles[currentEffectSpawnIndex].isGround)
            {
                CoreEffectManager.StartEffects(currParticles[currentEffectSpawnIndex].Object, CoreCollisionSenses.GroundCheck.position);
            }
            else
            {
                if (currParticles[currentEffectSpawnIndex].isRandomPosRot)
                {
                    CoreEffectManager.StartEffectsWithRandomPosRot(
                            currParticles[currentEffectSpawnIndex].Object, 
                            currParticles[currentEffectSpawnIndex].isRandomRange);
                }
                else
                {
                    CoreEffectManager.StartEffects(currParticles[currentEffectSpawnIndex].Object);
                }
            }
        }
        protected override void Start()
        {
            base.Start();

            eventHandler.OnEffectSpawn += HandleEffectSpawn;
        }

        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnEffectSpawn -= HandleEffectSpawn;
        }

    }
}
