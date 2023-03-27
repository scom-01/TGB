using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class SoundClip : WeaponComponent<SoundClipData, ActionSoundClip>
    {
        private ActionHitBox hitBox;
        private int currentSoundIndex;
        private int currentSoundCommandIndex;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentSoundCommandIndex = 0;
            currentSoundIndex = 0;
        }
        public void HandleSoundClip()
        {
            if (currentGroundedActionData != null && currentAirActionData != null)
            {
                if (weapon.InAir)
                {
                    CheckSoundClipAction(currentAirActionData);
                }
                else
                {
                    CheckSoundClipAction(currentGroundedActionData);
                }
            }
            else if (currentGroundedActionData == null)
            {
                CheckSoundClipAction(currentAirActionData);
            }
            else if (currentAirActionData == null)
            {
                CheckSoundClipAction(currentGroundedActionData);
            }

            currentSoundIndex++;
        }

        public void DetectedSoundClip(Collider2D[] coll)
        {
            foreach(var item in coll)
            {
                if (item.gameObject.tag == this.gameObject.tag)
                    continue;
                
            }
        }
        private void CheckSoundClipAction(ActionSoundClip actionSoundClip)
        {
            if (actionSoundClip == null)
                return;

            var currSoundClips = actionSoundClip.audioClips;
            if (currSoundClips.Length <= 0)
                return;

            for (int i = 0; i < currSoundClips.Length; i++)
            {
                if (currSoundClips[i].Command == weapon.Command)
                {
                    currentSoundCommandIndex = i;
                    break;
                }
                currentSoundCommandIndex = -1;
            }

            if (currentSoundCommandIndex == -1)
            {
                weapon.EventHandler.AnimationFinishedTrigger();
                return;
            }

            if (currentSoundIndex >= currSoundClips[currentSoundCommandIndex].audioClips.Length)
            {
                Debug.LogWarning(currSoundClips + "[" + currentSoundCommandIndex + "].audioClips is Null");
                return;
            }

            core.GetCoreComponent<SoundEffect>().AudioSpawn(currSoundClips[currentSoundCommandIndex].audioClips[currentSoundIndex]);
        }
        protected override void Awake()
        {
            base.Awake();
            hitBox = GetComponent<ActionHitBox>();
            hitBox.OnDetectedCollider2D += DetectedSoundClip;
        }

        protected override void Start()
        {
            base.Start();
            eventHandler.OnSoundClip += HandleSoundClip;
        }
        protected override void OnDestory()
        {
            base.OnDestory();
            hitBox.OnDetectedCollider2D -= DetectedSoundClip;
            eventHandler.OnSoundClip -= HandleSoundClip;
        }
    }
}
