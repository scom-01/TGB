using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount);
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount, int repeat);
    public void HitEffect(GameObject EffectPrefab, float Range, int FancingDirection);
}
