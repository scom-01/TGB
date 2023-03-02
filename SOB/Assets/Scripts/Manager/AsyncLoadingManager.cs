using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SOB.Manager
{
    public class AsyncLoadingManager : MonoBehaviour
    {
        public UIEventHandler UIEvent;
        public string SceneName;

        public float Timer;
        private void Start()
        {
            StartCoroutine(LoadAsyncSceneCoroutine());
        }

        IEnumerator LoadAsyncSceneCoroutine()
        {
            var time = 0.0f;
            AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                time += Time.time;
                if (UIEvent != null)
                {
                    if (UIEvent.Loading_progressbar != null)
                    {

                        UIEvent.Loading_progressbar.value = time / Timer;
                        Debug.Log(UIEvent.Loading_progressbar.value);
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
