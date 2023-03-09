using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class ParticleSpawn : WeaponComponent<ParticleSpawnData, ActionParticle>
    {
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement
        {
            get => coreMovement ? coreMovement : core.GetCoreComponent(ref coreMovement);
        }
        private CoreSystem.CollisionSenses coreCollisionSenses;
        private CoreSystem.CollisionSenses CoreCollisionSenses
        {
            get => coreCollisionSenses ? coreCollisionSenses : core.GetCoreComponent(ref coreCollisionSenses);
        }

        private int currentParticleSpawnIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentParticleSpawnIndex = 0;
        }

        private void HandleParticleSpawn()
        {
            GameObject[] currentParticle = new GameObject[0];
            currentParticle = currentActionData.ParticlePrefabs;
            if (currentParticle.Length < 0)
                return;

            if (currentParticleSpawnIndex >= currentParticle.Length)
            {
                Debug.Log($"{weapon.name} Particle Prefabs length mismatch");
                return;
            }

            Instantiate(currentActionData.ParticlePrefabs[currentParticleSpawnIndex], CoreCollisionSenses.GroundCheck.position, Quaternion.identity);
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
