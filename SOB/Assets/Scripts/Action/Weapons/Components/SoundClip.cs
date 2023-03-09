using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class SoundClip : WeaponComponent<SoundClipData, ActionSoundClip>
    {
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

        protected override void Start()
        {
            base.Start();
            eventHandler.OnSoundClip += HandleSoundClip;
        }
        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnSoundClip -= HandleSoundClip;
        }
    }
}
