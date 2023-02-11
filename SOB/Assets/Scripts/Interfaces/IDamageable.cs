using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// attacker가  victim에게 amount만큼의 피해를 줌
    /// </summary>
    /// <param name="attacker">공격하는 Object</param>
    /// <param name="victim">피해받는 Object</param>
    /// <param name="elementalPower">공격하는 원소 속성</param>
    /// <param name="attiribute">물리, 마법</param>
    /// <param name="amount">피해량</param>
    public void Damage(GameObject attacker, GameObject victim, ElementalPower elementalPower, DamageAttiribute attiribute, float amount);
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, GameObject EffectPrefab, float amount);
}
