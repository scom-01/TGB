using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CommonData
{
    [Tooltip("최대 체력")]
    public float maxHealth;
    [Tooltip("움직임 Velocity")]
    public float movementVelocity;
    [Tooltip("기본 공격력")]
    public float AttackPower;
    [Tooltip("기본 방어력")]
    public float DefendsivePower;
}
