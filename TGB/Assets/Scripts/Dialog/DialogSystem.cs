using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct Speaker
{
    public SpriteRenderer spriteRenderer;   //캐릭터 이미지
    public Image imageDialog;               //대화창 ImageUI
    public TextMeshProUGUI textName;        //현재 대사중인 캐릭터 이름 출력 Text UI
    public TextMeshProUGUI textDialogue;    //현재 대사 출력 Text UI
    public GameObject objectArrow;          //대사가 완료되었을 때 출력되는 커서 오브젝트
}
[Serializable]
public struct DialogData
{
    public int speakerIdx;          //이름과 대사를 출력ㄷ할 현재 DialogSystem의 speakers 배열 순서
    public string name;             //캐릭터 이름
    [TextArea(3, 5)]
    public string dialog;           //대사
}


namespace SCOM.Dialog
{
    public class DialogSystem : MonoBehaviour
    {
        [SerializeField]
        private Speaker[] speakers;         //대화에 참여하는 캐릭터들의 UI배열
        [SerializeField]
        private DialogData[] dialogs;        //현재 분기의 대사 목록 배열
        [SerializeField]
        private bool isAutoStart = true;    //자동 시작 여부
        private bool isFirst = true;        //최초 1회만 호출하기 위한 변수
        private int currentDialogIdx = -1;  //현재 대사 순번
        private int currentSpeakerIdx = 0;  //현재 말을 하는 화자(speaker)의 spaekers 배열 순서
        [SerializeField] private float typingSpeed = 0.1f;   //텍스트 타이핑 효과의 재생속도
        [SerializeField] private bool isTypingEffect = false;//텍스트 타이핑 효과를 재생 중인지
        private void Awake()
        {
            SetUp();
        }
        private void SetUp()
        {
            //모든 대화관련 게임오브젝트 비활성화
            for (int i = 0; i < speakers.Length; ++i)
            {
                SetActiveObjects(speakers[i], false);
                //캐릭터 이미지는 보이도록
                speakers[i].spriteRenderer.gameObject.SetActive(true);
            }
        }

        public bool UpdateDialog()
        {
            if (isFirst)
            {
                SetUp();

                if (isAutoStart)
                {
                    SetNextDialog();
                }

                isFirst = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                //텍스트 타이핑 효과를 재생중일 때 마우스 왼쪽 클릭하면 타이핑 효과 종료
                if (isTypingEffect)
                {
                    isTypingEffect = false;

                    StopCoroutine("OnTypingText");
                    speakers[currentSpeakerIdx].textDialogue.text = dialogs[currentDialogIdx].dialog;
                    speakers[currentSpeakerIdx].objectArrow.SetActive(true);
                    return false;
                }

                //대사가 남아있는 경우
                if (dialogs.Length > currentDialogIdx + 1)
                {
                    SetNextDialog();
                }
                //대사가 남아있지 않는 경우
                else
                {
                    for (int i = 0; i < speakers.Length; ++i)
                    {
                        SetActiveObjects(speakers[i], false);
                        speakers[i].spriteRenderer.gameObject.SetActive(false);
                    }
                    return true;
                }
            }
            return false;
        }

        private void SetNextDialog()
        {
            //이전 화자의 대화관련 오브젝트 비활성화
            SetActiveObjects(speakers[currentSpeakerIdx], false);
            //다음 대사를 진행하도록
            currentDialogIdx++;
            //현재 화자 순번 설정
            currentSpeakerIdx = dialogs[currentDialogIdx].speakerIdx;

            //현재 화자의 대화 관련 오브젝트 활성화
            SetActiveObjects(speakers[currentSpeakerIdx], true);
            //이름과 대사 설정
            speakers[currentSpeakerIdx].textName.text = dialogs[currentDialogIdx].name;
            //speakers[currentSpeakerIdx].textDialogue.text = dialog[currentDialogIdx].dialog;
            StartCoroutine("OnTypingText");
        }

        private void SetActiveObjects(Speaker speaker, bool visible)
        {
            speaker.imageDialog.gameObject.SetActive(visible);
            speaker.textName.gameObject.SetActive(visible);
            speaker.textDialogue.gameObject.SetActive(visible);

            //화살표는 대사가 종료되었을 때만 활성화하기 때문에 항상 false
            speaker.objectArrow.gameObject.SetActive(false);

            //캐릭터 알파값 변경
            Color color = speaker.spriteRenderer.color;
            color.a = visible == true ? 1f : 0.2f;
            speaker.spriteRenderer.color = color;
        }

        private IEnumerator OnTypingText()
        {
            int index = 0;

            isTypingEffect = true;

            while (index <= dialogs[currentDialogIdx].dialog.Length)
            {
                speakers[currentSpeakerIdx].textDialogue.text = dialogs[currentDialogIdx].dialog.Substring(0, index);

                index++;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTypingEffect = false;

            //커서이미지 활성화
            speakers[currentSpeakerIdx].objectArrow.SetActive(true);
        }
    }
}
