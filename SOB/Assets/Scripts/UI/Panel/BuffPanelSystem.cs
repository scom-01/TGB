using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BuffPanelSystem : MonoBehaviour
{
    private Player player;

    public List<Buff> buffs;
    [SerializeField] private GameObject buffVertical;

    [Header("Prefab")]
    [SerializeField] private GameObject buffPanelHorizontalPrefab;
    [SerializeField] private GameObject buffPanelPrefab;

    [SerializeField] private int MaxBuffPanelHorizontal;

    private void Awake()
    {
        this.player = GameManager.Inst.player.GetComponent<Player>();
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    public void BuffPanelAdd(Buff buff)
    {
        if(buffVertical.GetComponentsInChildren<BuffPanelItem>().Length > 0)
        {

        }
        else
        {
            GameObject horizontal = Instantiate(buffPanelHorizontalPrefab, buffVertical.transform);
            GameObject item = Instantiate(buffPanelPrefab, horizontal.transform);
            //item.GetComponent<BuffPanelItem>().BuffDurationTime = buff.BuffItemData.DurationTime;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        BuffPanelUpdate();
    }

    public void BuffPanelUpdate()
    {
        for (int i = 0; i < buffs.Count; i++)
        {            
            if (Time.time >= buffs[i].durationTime + buffs[i].startTime)
            {
                //buffs[i].
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
