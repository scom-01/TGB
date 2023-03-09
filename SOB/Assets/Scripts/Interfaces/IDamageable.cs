using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount);
    public void HitAction(GameObject EffectPrefab, float Range);
}
