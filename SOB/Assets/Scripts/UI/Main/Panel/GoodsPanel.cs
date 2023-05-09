using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoodsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GoldCountText;
    [SerializeField] private TextMeshProUGUI sculptureCountText;

    private int currentGold;
    private int currentSculpture;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Inst != null)
        {
            currentGold = DataManager.Inst.GoldCount;
            currentSculpture = DataManager.Inst.ElementalsculptureCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DataManager.Inst != null)
        {
            currentGold = DataManager.Inst.GoldCount;
            currentSculpture = DataManager.Inst.ElementalsculptureCount;
        }

        GoldCountText.text = currentGold.ToString() + " :";
        sculptureCountText.text = currentSculpture.ToString() + " :";
    }
}
