using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    /*Param*/

    public static float GV_SystemDeltatime = 0.02f;
    public static float GV_CharacterDeltatime = 0.02f;

    public const string SoundContainerTagName = "SoundContainer";

    #region PlayerPrefs Data
    public const string StageName = "StageName";
    public const string NextStageName = "NextStage";
    public const string RebindsKey = "rebinds";
    /// <summary>
    /// AudioMixer의 SFX Parameter값과 일치해야함
    /// </summary>
    public const string SFX_Vol = "SFX_Vol";
    /// <summary>
    /// AudioMixer의 BGM Parameter값과 일치해야함
    /// </summary>
    public const string BGM_Vol = "BGM_Vol";
    public const string Quality = "Quality";
    public const string Language = "Language";

    public const string UI_Table = "Localization/StringTables/UI Table";

    public const string FadeInCutScene = "TimeLine/FadeIn";
    public const string FadeOutCutScene = "TimeLine/FadeOut";

    public const string GoldCount = "GoldCount";
    public const string ElementalCount = "ElementalCount";

    #endregion
    /// <summary>
    /// Elemetal 속성 공격에 따른 추가 데미지 퍼센트
    /// ex) 0.3f일때 Weak Elemental에 대한 추가 데미지는 30%이다.
    /// </summary>
    public static float E_WeakPer = 0.3f;

    /// <summary>
    /// Enemy의 Size에 따른 추가 데미지 퍼센트
    /// Player의 사이즈는 Medium으로 정의하여 계산
    /// ex) 0.3f일때 Small Enemy에 대한 추가 데미지는 30%이다.
    /// ex) 0.3f일때 Big Enemy에 대해 받는 추가 데미지는 30%이다.
    /// </summary>
    public static float Enemy_Size_WeakPer = 0.1f;
    /*~Param*/

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GlobalValue Instance
    {
        get
        {
            if (null == Inst)
            {
                return null;
            }
            return Inst;
        }
    }

    /*func*/
    public float GetSystemDelta() { return GV_SystemDeltatime; }
    public void SetSystemDelta(float deltatime)
    {
        if (GV_SystemDeltatime != deltatime)
        {
            GV_SystemDeltatime = deltatime;
        }
    }
    public float GetCharacterDelta() { return GV_CharacterDeltatime; }
    public void SetCharacterDelta(float deltatime)
    {
        if(GV_CharacterDeltatime != deltatime)
        {
            GV_CharacterDeltatime = deltatime;
        }
    }
    /*~func*/
}

