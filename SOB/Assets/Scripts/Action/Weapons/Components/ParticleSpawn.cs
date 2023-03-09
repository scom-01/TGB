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
            EffectPrefab[] currentParticle = new EffectPrefab[0];
            currentParticle = currentActionData.ParticlePrefabs;
            if (currentParticle.Length < 0)
                return;

            if (currentParticleSpawnIndex >= currentParticle.Length)
            {
                Debug.Log($"{weapon.name} Particle Prefabs length mismatch");
                return;
            }

            if (currentActionData.ParticlePrefabs[currentParticleSpawnIndex].isGround)
            {   
                CoreParticleManager.StartParticles(currentActionData.ParticlePrefabs[currentParticleSpawnIndex].Object, CoreCollisionSenses.GroundCheck.position, Quaternion.identity);
            }
            else
            {
                if(currentActionData.ParticlePrefabs[currentParticleSpawnIndex].isRandomPosRot)
                {
                    CoreParticleManager.StartParticlesWithRandomPosRot(currentActionData.ParticlePrefabs[currentParticleSpawnIndex].Object, currentActionData.ParticlePrefabs[currentParticleSpawnIndex].isRandomRange);
                }
                else
                {
                    CoreParticleManager.StartParticles(currentActionData.ParticlePrefabs[currentParticleSpawnIndex].Object);
                }
            }
            currentParticleSpawnIndex++;
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
