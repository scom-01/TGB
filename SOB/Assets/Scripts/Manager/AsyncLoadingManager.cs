using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SOB.Manager
{
    public class AsyncLoadingManager : MonoBehaviour
    {
        public UIEventHandler UIEvent;        

        public float Timer;
        private void Start()
        {
            StartCoroutine(LoadAsyncSceneCoroutine());
        }

        IEnumerator LoadAsyncSceneCoroutine()
        {
            var time = 0.0f;
            Debug.Log($"Scene Load {DataManager.Inst.SceneName}");
            AsyncOperation operation = SceneManager.LoadSceneAsync(DataManager.Inst.SceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                time += Time.time;
                if (UIEvent != null)
                {
                    if (UIEvent.Loading_progressbar != null)
                    {

                        UIEvent.Loading_progressbar.value = time / Timer;
                    }
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
