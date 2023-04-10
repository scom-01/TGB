using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SOB.Dialog
{
    public class DialogTest : MonoBehaviour
    {
        [SerializeField]
        private DialogSystem dialogSystem1;
        [SerializeField]
        private TextMeshProUGUI textCountdown;
        [SerializeField]
        private DialogSystem dialogSystem2;
        // Start is called before the first frame update
        private IEnumerator Start()
        {
            textCountdown.gameObject.SetActive(false);

            //첫 번째 대사 분기
            yield return new WaitUntil(() => dialogSystem1.UpdateDialog());

            textCountdown.gameObject.SetActive(true);
            int count = 5;
            while(count > 0)
            {
                textCountdown.text = count.ToString();
                count--;
                yield return new WaitForSeconds(1);
            }
            textCountdown.gameObject.SetActive(false);

            yield return new WaitUntil(() => dialogSystem2.UpdateDialog());

            textCountdown.gameObject.SetActive(true);
            textCountdown.text = "The End";

            yield return new WaitForSeconds(2);

            UnityEditor.EditorApplication.ExitPlaymode();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
