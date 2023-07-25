using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount);
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, E_Power _elemental, float amount);
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount, int repeat);
    public void FixedDamage(int amount, bool isTrueHit = false);
    public void HitEffect(GameObject EffectPrefab, float Range, int FancingDirection);
}
