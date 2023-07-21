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
                Debug.Log($"{Weapon.name} Particle Prefabs length mismatch");
                return;
            }
               
            var offset = new Vector3(currParticles[currentEffectSpawnIndex].EffectRect.x * CoreMovement.FancingDirection, currParticles[currentEffectSpawnIndex].EffectRect.y);
            var size = new Vector2(currParticles[currentEffectSpawnIndex].EffectRect.width, currParticles[currentEffectSpawnIndex].EffectRect.height);
            if (currParticles[currentEffectSpawnIndex].isGround)
            {
                if(currParticles[currentEffectSpawnIndex].isRandomPosRot)
                {
                    CoreEffectManager.StartEffects(currParticles[currentEffectSpawnIndex].Object,
                        CoreCollisionSenses.GroundCheck.position, currParticles[currentEffectSpawnIndex].isFollowing);
                }
                else
                {
                    CoreEffectManager.StartEffects(currParticles[currentEffectSpawnIndex].Object,
                        CoreCollisionSenses.GroundCheck.position + offset, currParticles[currentEffectSpawnIndex].isFollowing);
                }
            }
            else
            {
                if (currParticles[currentEffectSpawnIndex].isRandomPosRot)
                {
                    CoreEffectManager.StartEffectsWithRandomPosRot(
                            currParticles[currentEffectSpawnIndex].Object, 
                            currParticles[currentEffectSpawnIndex].isRandomRange, currParticles[currentEffectSpawnIndex].isFollowing);
                }
                else
                {
                    CoreEffectManager.StartEffects(currParticles[currentEffectSpawnIndex].Object, this.transform.position + offset, currParticles[currentEffectSpawnIndex].isFollowing);
                }
            }
        }
        protected override void Start()
        {
            base.Start();

            eventHandler.OnEffectSpawn -= HandleEffectSpawn;
            eventHandler.OnEffectSpawn += HandleEffectSpawn;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            eventHandler.OnEffectSpawn -= HandleEffectSpawn;
        }

    }
}
