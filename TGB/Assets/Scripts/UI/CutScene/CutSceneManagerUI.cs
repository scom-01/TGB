using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace TGB.Manager
{
    public class CutSceneManagerUI : MonoBehaviour
    {
        public Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponent<Canvas>();
                }
                return canvas;
            }
        }
        private Canvas canvas;

        public PlayableAsset FadeIn;
        public PlayableAsset FadeOut;
        public PlayableDirector Director
        { 
            get
            {
                if (director == null)
                    director = this.GetComponentInChildren<PlayableDirector>();
                return director;
            }
        }
        private PlayableDirector director;

        public void Director_SetAsset(PlayableAsset playableAsset)
        {
            if (playableAsset == null)
                return;

            Director.playableAsset = playableAsset;            
        }
    }
}