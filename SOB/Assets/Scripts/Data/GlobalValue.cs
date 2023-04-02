using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    /*Param*/

    public static float GV_SystemDeltatime = 0.02f;
    public static float GV_CharacterDeltatime = 0.02f;

    //public static string FlashMtrlPath = "Material/FlashWhite";

    public const string RebindsKey = "rebinds";
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

