using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TGB
{
    public class SoundPooling : MonoBehaviour
    {
        public AudioPrefab SoundObject;
        public int MaxPoolAmount;
        protected Queue<AudioSource> ObjectQueue = new Queue<AudioSource>();
        public AudioSource CreateObject(AudioPrefab _SFX)
        {
            var source = this.AddComponent<AudioSource>();
            source.clip = _SFX.Clip;
            source.volume= _SFX.Volume;
            source.playOnAwake = false;
            source.outputAudioMixerGroup = DataManager.Inst.SFX;
            source.loop = false;
            source.Stop();
            return source;
        }

        public void Init(AudioPrefab _SFX, int count)
        {
            if (_SFX.Clip == null)
                return;

            SoundObject = _SFX;
            MaxPoolAmount = count;

            for (int i = 0; i < MaxPoolAmount; i++)
            {
                ObjectQueue.Enqueue(CreateObject(_SFX));
            }
        }

        public AudioSource GetObejct()
        {
            if (ObjectQueue.Count > 0)
            {
                var obj = ObjectQueue.Dequeue();
                if (obj != null)
                {
                    obj.Play();
                    StartCoroutine(ReturnObject(obj));
                }
                return obj;
            }
            else
            {
                var newobj = CreateObject(SoundObject);
                newobj.Play();
                StartCoroutine(ReturnObject(newobj));
                return newobj;
            }
        }

        public IEnumerator ReturnObject(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);

            if (ObjectQueue.Count >= MaxPoolAmount)
            {
                Destroy(source);
            }
            else
            {
                source.Stop();
                ObjectQueue.Enqueue(source);
            }
            yield return null;
        }
    }
}
