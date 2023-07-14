using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SOB.Manager
{
    public class AsyncLoadingManager : MonoBehaviour
    {
        [SerializeField] private Slider Slider;
        public float Timer;
        private void Start()
        {
            StartCoroutine(LoadAsyncSceneCoroutine());
        }

        IEnumerator LoadAsyncSceneCoroutine()
        {
            var time = 0.0f;
            Debug.Log($"Scene Load {DataManager.Inst.SceneIdx}");
            AsyncOperation operation = SceneManager.LoadSceneAsync(DataManager.Inst.SceneIdx);            
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                time += Time.time;

                if (Slider != null)
                {
                    Slider.value = time / Timer;
                }

                if (time > Timer)
                {
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}
