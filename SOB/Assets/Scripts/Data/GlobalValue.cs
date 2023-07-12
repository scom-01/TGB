using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    /*Param*/
    public static float GravityPower = 5f;

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

    public const string UI_Table = "Localization/StringTables/UI_Table";

    public const string FadeInCutScene = "TimeLine/FadeIn";
    public const string FadeOutCutScene = "TimeLine/FadeOut";

    public const string GoldCount = "GoldCount";
    public const string ElementalCount = "ElementalCount";

    #endregion

    #region UI Icon Path
    public const string Icon_UI_Path = "ScriptPath/UI/Goods";
    public const string Icon_Gold_Path = "GoldIcon";
    public const string Icon_FireGoods_Path = "FireGoods";
    public const string Icon_WaterGoods_Path = "WaterGoods";
    public const string Icon_EarthGoods_Path = "EarthGoods";
    public const string Icon_WindGoods_Path = "WindGoods";

    public const string Sprites_UI_Path = "ScriptPath/UI/Sprites";
    public const string Symbol_Gold_Path = "Symbol_Gold";
    public const string Symbol_Fire_Path = "Symbol_Fire";
    public const string Symbol_Water_Path = "Symbol_Water";
    public const string Symbol_Earth_Path = "Symbol_Earth";
    public const string Symbol_Wind_Path = "Symbol_Wind";

    public const string StatsSprites_UI_Path = "Stats_Sheet";
    #endregion

    #region UI Sound Path
    public const string Sounds_UI_Path = "ScriptPath/UI/Sounds/";
    public const string Reforging_Success = "Reforging_Success";
    public const string Reforging_Failure = "Reforging_Failure";
    #endregion

    #region DB_Path
    public static ItemDB All_ItemDB = Resources.Load<ItemDB>("DB/All_ItemDB");
    public static ItemDB Lock_ItemDB = Resources.Load<ItemDB>("DB/Lock_ItemDB");
    public static WeaponDB All_WeaponDB = Resources.Load<WeaponDB>("DB/All_WeaponDB");

    public static int[] DefaultUnlockItem = { 0, 1, 2, 3, 4, 5, 6, };
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
        if (GV_CharacterDeltatime != deltatime)
        {
            GV_CharacterDeltatime = deltatime;
        }
    }
    /*~func*/
}

