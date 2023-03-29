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
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentSoundIndex = 0;
        }
        public void HandleSoundClip()
        {
            if(currentActionData !=null)
            {
                CheckSoundClipAction(currentActionData);
            }
            //if (currentActionData != null && currentAirActionData != null)
            //{
            //    if (weapon.InAir)
            //    {
            //        CheckSoundClipAction(currentAirActionData);
            //    }
            //    else
            //    {
            //        CheckSoundClipAction(currentActionData);
            //    }
            //}
            //else if (currentActionData == null)
            //{
            //    CheckSoundClipAction(currentAirActionData);
            //}
            //else if (currentAirActionData == null)
            //{
            //    CheckSoundClipAction(currentActionData);
            //}

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

            if (currentSoundIndex >= currSoundClips.Length)
            {
                Debug.LogWarning(currSoundClips + "[" + currentSoundIndex + "].audioClips is Null");
                return;
            }

            core.GetCoreComponent<SoundEffect>().AudioSpawn(currSoundClips[currentSoundIndex]);
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
