using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TGB.Manager
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
            CheckSkipCutScene(DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneNumber);
            var time = 0.0f;
            Debug.Log($"Scene Load {DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneNumber}");
            AsyncOperation operation = SceneManager.LoadSceneAsync(DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneNumber);            
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
                Debug.LogWarning($"Scene Loading..");
                yield return null;
            }
        }

        private void CheckSkipCutScene(int idx)
        {
            var SkipCutScene = DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.SkipCutSceneList.ToList();
            if(SkipCutScene.Contains(idx))
            {
                CheckSkipCutScene(idx + 1);
                return;
            }
            DataManager.Inst.NextStage(idx);
        }
    }
}
