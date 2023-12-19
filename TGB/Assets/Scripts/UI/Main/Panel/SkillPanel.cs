using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    private Unit Unit
    {
        get
        {
            if(unit == null)
            {
                if (GameManager.Inst?.StageManager != null)
                {
                    unit = GameManager.Inst.StageManager.player;
                }
            }
            return unit;
        }
    }
    private Unit unit;
    public Image Skill_Img;
    public Image Skill_Img_Filled;
    public Sprite SkillSprite;
    private float StartTime = 0f;
    private float CooldownTime = 0f;
    public TMP_Text Skill_CooldownTime;
    private bool isUpdate = false;

    // Start is called before the first frame update
    void Start()
    {        
        if (Skill_Img_Filled != null)
            Skill_Img_Filled.fillAmount = 1f;

        if (Skill_CooldownTime != null)
            Skill_CooldownTime.text = "";
        isUpdate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Unit == null)
            return;

        if (GameManager.Inst?.StageManager == null)
            return;

        if (!isUpdate)
            return;

        if (GameManager.Inst.PlayTime < StartTime + CooldownTime)
        {
            var temp = (StartTime + CooldownTime - GameManager.Inst.PlayTime);
            if (temp > 1.0f)
            {
                Skill_CooldownTime.text = temp.ToString("F0") + "s";
            }
            else if(temp >= 0.0f)
            {
                Skill_CooldownTime.text = temp.ToString("F1") + "s";
            }
            Skill_Img_Filled.fillAmount = (1f - (StartTime + CooldownTime - GameManager.Inst.PlayTime) / CooldownTime);
        }
        else
        {
            isUpdate = false;
            Skill_CooldownTime.text = "";
            Skill_Img_Filled.fillAmount = 1f;
        }
    }

    public void UpdateSkillPanel(float _startTime, float _skillCooldowmTime)
    {
        StartTime = _startTime;
        CooldownTime = _skillCooldowmTime;        
        isUpdate = true;
    }

    public void Init(Sprite _SkillSprite)
    {
        if (_SkillSprite == null)
        {
            this.gameObject.SetActive(false);
            return;
        }
        SkillSprite = _SkillSprite;
        this.gameObject.SetActive(true);
    }
}
