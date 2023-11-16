using TGB.CoreSystem;
using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
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

            if (currParticles[currentEffectSpawnIndex].Object == null)
                return;

            var offset = new Vector3(currParticles[currentEffectSpawnIndex].EffectOffset.x * CoreMovement.FancingDirection, currParticles[currentEffectSpawnIndex].EffectOffset.y);
            var size = currParticles[currentEffectSpawnIndex].EffectScale;

            if(currParticles[currentEffectSpawnIndex].isTransformGlobal)
            {
                CoreEffectManager.StartEffectsPos(currParticles[currentEffectSpawnIndex].Object, Vector2.zero, size, false);
                return;
            }

            if(currParticles[currentEffectSpawnIndex].isHeader)
            {
                CoreEffectManager.StartEffectsPos(currParticles[currentEffectSpawnIndex].Object,
                    (currParticles[currentEffectSpawnIndex].isRandomPosRot ? CoreCollisionSenses.HeaderCenterPos : CoreCollisionSenses.HeaderCenterPos + offset), size, currParticles[currentEffectSpawnIndex].isFollowing);
            }
            else if (currParticles[currentEffectSpawnIndex].isGround)
            {
                CoreEffectManager.StartEffectsPos(currParticles[currentEffectSpawnIndex].Object,
                    (currParticles[currentEffectSpawnIndex].isRandomPosRot ? CoreCollisionSenses.GroundCenterPos : CoreCollisionSenses.GroundCenterPos + offset) , size, currParticles[currentEffectSpawnIndex].isFollowing);
            }
            else
            {
                if (currParticles[currentEffectSpawnIndex].isRandomPosRot)
                {
                    CoreEffectManager.StartEffectsWithRandomPosRot(
                            currParticles[currentEffectSpawnIndex].Object,
                            currParticles[currentEffectSpawnIndex].isRandomRange, size, currParticles[currentEffectSpawnIndex].isFollowing);
                }
                else
                {
                    CoreEffectManager.StartEffectsPos(currParticles[currentEffectSpawnIndex].Object, this.transform.position + offset, size, currParticles[currentEffectSpawnIndex].isFollowing);
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
