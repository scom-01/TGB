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
        private int currentParticleCommandIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentParticleCommandIndex = 0;
            currentParticleSpawnIndex = 0;
        }

        private void HandleParticleSpawn()
        {
            if (currentGroundedActionData != null && currentAirActionData != null)
            {
                if (weapon.InAir)
                {
                    CheckParticleAction(currentAirActionData);
                }
                else
                {
                    CheckParticleAction(currentGroundedActionData);
                }
            }
            else if (currentGroundedActionData == null)
            {
                CheckParticleAction(currentAirActionData);
            }
            else if (currentAirActionData == null)
            {
                CheckParticleAction(currentGroundedActionData);
            }
            currentParticleSpawnIndex++;
        }

        private void CheckParticleAction(ActionParticle actionParticle)
        {
            if (actionParticle == null)
                return;

            var currParticles = actionParticle.ParticleCommands;
            if (currParticles.Length <= 0)
                return;

            for (int i = 0; i < currParticles.Length; i++)
            {
                if (currParticles[i].Command == weapon.Command)
                {
                    currentParticleCommandIndex = i;
                    break;
                }
                currentParticleCommandIndex = -1;
            }

            if (currentParticleCommandIndex == -1)
            {
                weapon.EventHandler.AnimationFinishedTrigger();
                return;
            }

            if (currentParticleSpawnIndex >= currParticles[currentParticleCommandIndex].effectPrefabs.Length)
            {
                Debug.Log($"{weapon.name} Particle Prefabs length mismatch");
                return;
            }

            if (currParticles[currentParticleCommandIndex].effectPrefabs[currentParticleSpawnIndex].isGround)
            {
                CoreParticleManager.StartParticles(currParticles[currentParticleCommandIndex].effectPrefabs[currentParticleSpawnIndex].Object, CoreCollisionSenses.GroundCheck.position);
            }
            else
            {
                if (currParticles[currentParticleCommandIndex].effectPrefabs[currentParticleSpawnIndex].isRandomPosRot)
                {
                    CoreParticleManager.StartParticlesWithRandomPosRot(
                            currParticles[currentParticleCommandIndex].effectPrefabs[currentParticleSpawnIndex].Object, 
                            currParticles[currentParticleCommandIndex].effectPrefabs[currentParticleSpawnIndex].isRandomRange);
                }
                else
                {
                    CoreParticleManager.StartParticles(currParticles[currentParticleCommandIndex].effectPrefabs[currentParticleSpawnIndex].Object);
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
