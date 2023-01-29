using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType
{
    Normal = 0,
    Fire = 1,
    Ice = 2,
    Poison = 3,
}

public enum EffectColor
{
    Black = 0,
    Red = 1,
    Blue = 2,
    Green = 3,
    Yellow = 4,
}

public enum ItemGetType
{ 
    Conflict = 0,
    DetectedSense = 1,
}

public class ItemValue : MonoBehaviour
{
    private SwordType   ElementalType;
    private int         Price;
    private int         Damage;
    private float       AttackSpeed;
    private float       Weight;
    private EffectColor Color;    
}
