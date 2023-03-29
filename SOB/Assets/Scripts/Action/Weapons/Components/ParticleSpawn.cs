using SOB.CoreSystem;
using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class ParticleSpawn : WeaponComponent<ParticleSpawnData, ActionParticle>
    {

        private int currentParticleSpawnIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentParticleSpawnIndex = 0;
        }

        private void HandleParticleSpawn()
        {
            if (currentActionData != null)
            {
                CheckParticleAction(currentActionData);
            }
            //if (currentActionData != null && currentAirActionData != null)
            //{
            //    if (weapon.InAir)
            //    {
            //        CheckParticleAction(currentAirActionData);
            //    }
            //    else
            //    {
            //        CheckParticleAction(currentActionData);
            //    }
            //}
            //else if (currentActionData == null)
            //{
            //    CheckParticleAction(currentAirActionData);
            //}
            //else if (currentAirActionData == null)
            //{
            //    CheckParticleAction(currentActionData);
            //}
            currentParticleSpawnIndex++;
        }

        private void CheckParticleAction(ActionParticle actionParticle)
        {
            if (actionParticle == null)
                return;

            var currParticles = actionParticle.EffectParticles;
            if (currParticles.Length <= 0)
                return;


            if (currentParticleSpawnIndex >= currParticles.Length)
            {
                Debug.Log($"{weapon.name} Particle Prefabs length mismatch");
                return;
            }

            if (currParticles[currentParticleSpawnIndex].isGround)
            {
                CoreParticleManager.StartParticles(currParticles[currentParticleSpawnIndex].Object, CoreCollisionSenses.GroundCheck.position);
            }
            else
            {
                if (currParticles[currentParticleSpawnIndex].isRandomPosRot)
                {
                    CoreParticleManager.StartParticlesWithRandomPosRot(
                            currParticles[currentParticleSpawnIndex].Object, 
                            currParticles[currentParticleSpawnIndex].isRandomRange);
                }
                else
                {
                    CoreParticleManager.StartParticles(currParticles[currentParticleSpawnIndex].Object);
                }
            }
        }
        protected override void Start()
        {
            base.Start();

            eventHandler.OnParticleSpawn += HandleParticleSpawn;
        }

        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnParticleSpawn -= HandleParticleSpawn;
        }

    }
}
