using System.Collections;
using TMPro;
using UnityEngine;

namespace SOB.Manager
{
    public class MainUIManager : MonoBehaviour
    {
        private GameObject Player;

        [SerializeField]
        private TextMeshProUGUI[] textUIs;

        private void Awake()
        {
            Player = GameManager.Inst?.player;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}