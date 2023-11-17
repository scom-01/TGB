using UnityEngine;

public interface IDamageable
{
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount);
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, E_Power _elemental, float amount);
    public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount, int repeat);
    public void TypeCalDamage(Unit AttackerUnit, Unit VictimUnit, float AttackerDmg, int RepeatAmount = 1);
    public bool FixedDamage(int amount, bool isTrueHit = false, int RepeatAmount = 1);
    public void HitEffect(GameObject EffectPrefab, float Range, int FancingDirection, Vector3 size);
    public void HitEffectRandRot(GameObject EffectPrefab, float Range, Vector3 size, bool isFollow = false);
}
