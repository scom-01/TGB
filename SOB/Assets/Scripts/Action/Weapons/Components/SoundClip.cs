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
            var currSoundClip = currentActionData.audioClips;
            if (currSoundClip.Length <= 0) 
            {
                Debug.Log("SoundClip is Null");
                return;
            }
            core.GetCoreComponent<SoundEffect>().AudioSpawn(currentActionData.audioClips[currentSoundIndex]);
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
