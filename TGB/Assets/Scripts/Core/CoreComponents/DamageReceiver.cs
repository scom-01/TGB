using System.Drawing;
using UnityEngine;

namespace TGB.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        public GameObject DefaultEffectPrefab;

        private CoreComp<UnitStats> stats;
        private CoreComp<EffectManager> effectManager;
        private CoreComp<Death> death;
        private BoxCollider2D BC2D;

        //객체가 이미 Hit를 했는지 판별(ex. true면 이미 피격당했다고 생각함)
        public bool isHit
        {
            get
            {
                if (core.Unit.isFixed_Hit_Immunity)
                {
                    return true;
                }

                return ishit;
            }
            set
            {
                if (core.Unit.isFixed_Hit_Immunity)
                {
                    ishit = true;
                }
                else
                {
                    ishit = value;
                }
            }
        }
        private bool ishit = false;
        public bool isTouch
        {
            get => istouch;
            set
            {
                istouch = value;
            }
        }
        private bool istouch = false;

        public float Damage(Unit attacker, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return 0f;
            }

            if (isHit)
            {
                Debug.Log(core.Unit.name + " isHit = true");
                return 0f;
            }
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");

            bool isCritical = false;
            //크리티컬 계산
            if (attacker.Core.CoreUnitStats.CalculStatsData.CriticalPer >= Random.Range(0, 100.0f))
            {
                isCritical = true;
                amount *= 1f + (attacker.Core.CoreUnitStats.CalculStatsData.AdditionalCriticalPer / 100.0f);
            }

            var damage = stats.Comp.DecreaseHealth(attacker, attacker.Core.CoreUnitStats.CalculStatsData.Elemental, attacker.Core.CoreUnitStats.CalculStatsData.DamageAttiribute, attacker.Core.CoreUnitStats.CalculStatsData.DefaultPower + amount);
            isHit = true;
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            HUD_DmgTxt(1.0f, damage, 50, attacker.Core.CoreUnitStats.CalculStatsData.DamageAttiribute, isCritical);
            return damage;
        }
        public float Damage(Unit attacker, E_Power _elemental, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return 0f;
            }

            if (isHit)
            {
                Debug.Log(core.Unit.name + " isHit = true");
                return 0f;
            }
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");

            bool isCritical = false;
            //크리티컬 계산
            if (attacker.Core.CoreUnitStats.CalculStatsData.CriticalPer >= Random.Range(0, 100.0f))
            {
                isCritical = true;
                amount *= 1f + (attacker.Core.CoreUnitStats.CalculStatsData.AdditionalCriticalPer / 100.0f);
            }

            var damage = stats.Comp.DecreaseHealth(attacker, _elemental, attacker.Core.CoreUnitStats.CalculStatsData.DamageAttiribute, attacker.Core.CoreUnitStats.CalculStatsData.DefaultPower + amount);
            isHit = true;
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            HUD_DmgTxt(1.0f, damage, 50, attacker.Core.CoreUnitStats.CalculStatsData.DamageAttiribute, isCritical);
            return damage;
        }
        public float Damage(Unit attacker, float amount, int repeat)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return 0f;
            }

            if (isHit)
                return 0f;

            float temp = 0f;
            for (int i = 0; i < repeat; i++)
            {
                temp += TrueDamage(attacker, amount);
            }
            isHit = true;
            return temp; 
        }
        /// <summary>
        /// 히트 무적 무시
        /// </summary>
        /// <param name="AttackterCommonData"></param>
        /// <param name="VictimCommonData"></param>
        /// <param name="amount"></param>
        public float TrueDamage(Unit attacker , float amount)
        {
            return TrueDamage(attacker, attacker.Core.CoreUnitStats.CalculStatsData.Elemental, attacker.Core.CoreUnitStats.CalculStatsData.DamageAttiribute, amount);
        }
        public float TrueDamage(Unit attacker, E_Power _Elemental, DAMAGE_ATT attribute, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return 0f;
            }

            bool isCritical = false;
            //크리티컬 계산
            if (attacker.Core.CoreUnitStats.CalculStatsData.CriticalPer >= Random.Range(0, 100.0f))
            {
                isCritical = true;
                amount *= 1f + (attacker.Core.CoreUnitStats.CalculStatsData.AdditionalCriticalPer / 100.0f);
            }

            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            var damage = stats.Comp.DecreaseHealth(attacker, _Elemental, attribute, amount);
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            HUD_DmgTxt(1.0f, damage, 50, attacker.Core.CoreUnitStats.CalculStatsData.DamageAttiribute, isCritical);
            return damage;
        }

        public float TypeCalDamage(Unit AttackerUnit, float AttackerDmg, int RepeatAmount = 1)
        {
            if (core.Unit.gameObject.tag == "Player")
            {
                return Damage
                    (
                    AttackerUnit,
                    AttackerDmg,
                    RepeatAmount
                    );
            }
            else
            {
                //Damage
                switch ((core.Unit as Enemy).enemyData.enemy_size)
                {
                    case ENEMY_Size.Small:
                        Debug.Log("Projectile Enemy Type Small, Normal Dam = " + AttackerDmg +
                            " Enemy_Size_WeakPer Additional Dam = " + (AttackerDmg) * (1.0f - GlobalValue.Enemy_Size_WeakPer));

                        return Damage
                        (
                        AttackerUnit,
                        (AttackerDmg) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                        RepeatAmount
                        );
                    case ENEMY_Size.Medium:
                        Debug.Log("Projectile Enemy Type Medium, Normal Dam = " + AttackerUnit.Core.CoreUnitStats.CalculStatsData.DefaultPower);

                        return Damage
                        (
                        AttackerUnit,
                        (AttackerDmg), RepeatAmount
                        );
                    case ENEMY_Size.Big:
                        Debug.Log("Projectile Enemy Type Big, Normal Dam = " + AttackerDmg +
                            " Enemy_Size_WeakPer Additional Dam = " +(AttackerDmg) * (1.0f - GlobalValue.Enemy_Size_WeakPer));

                        return Damage
                        (
                        AttackerUnit,
                        (AttackerDmg) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                        RepeatAmount);
                    default:
                        Debug.Log("Projectile Enemy Type Medium, Normal Dam = " + AttackerUnit.Core.CoreUnitStats.CalculStatsData.DefaultPower);

                        return Damage
                        (
                        AttackerUnit,
                        (AttackerDmg), RepeatAmount
                        );
                }
            }
        }

        public float FixedDamage(Unit attacker, int amount, bool isTrueHit = false, int RepeatAmount = 1)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return 0f;
            }

            if (isTrueHit)
            {
                Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
                var damage = stats.Comp.DecreaseHealth(attacker, amount);
                HUD_DmgTxt(1.0f, damage, 50, DAMAGE_ATT.Fixed, false);
                if (RepeatAmount > 1)
                {
                    var temp = RepeatAmount - 1;
                    return FixedDamage(attacker, amount, isTrueHit, temp) + damage;
                }
                stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
                return damage;
            }
            else
            {
                if (isHit)
                {
                    Debug.Log(core.Unit.name + " isHit = true");
                    return 0f;
                }
                Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
                var damage = stats.Comp.DecreaseHealth(attacker, amount);
                HUD_DmgTxt(1.0f, damage, 50, DAMAGE_ATT.Fixed, false);
                isHit = true;
                stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
                return damage;
            }
        }
        /// <summary>
        /// 고정 데미지
        /// </summary>
        /// <param name="amount">데미지 량</param>
        /// <param name="isTrueHit">피격 후 무적판정 무시공격(true)</param>
        /// <param name="RepeatAmount">반복 횟수(isTrueHit = false 때는 반영되지 않음)</param>
        public float FixedDamage(int amount, bool isTrueHit = false, int RepeatAmount = 1)
        {
            return FixedDamage(null, amount ,isTrueHit,RepeatAmount);
        }
        public float TrapDamage(StatsData AttackterCommonData, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return 0f;
            }
            if (isTouch)
            {
                return 0f;
            }

            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            isTouch = true;
            var damage = stats.Comp.DecreaseHealth(AttackterCommonData.Elemental, AttackterCommonData.DamageAttiribute, amount);
            stats.Comp.TouchinvincibleTime = core.Unit.UnitData.touchDamageinvincibleTime;

            HUD_DmgTxt(1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
            return damage;
        }
        public void HitEffect(GameObject EffectPrefab, float Range, int FancingDirection, Vector3 size)
        {
            if (EffectPrefab == null)
            {
                if (DefaultEffectPrefab != null)
                {
                    effectManager.Comp.StartEffectsWithRandomPos(DefaultEffectPrefab, Range, FancingDirection, size);
                }
                return;
            }

            effectManager.Comp.StartEffectsWithRandomPos(EffectPrefab, Range, FancingDirection, size);
        }
        public void HitEffectRandRot(GameObject EffectPrefab, float Range, Vector3 size, bool isFollow = false)
        {
            if (EffectPrefab == null)
            {
                if (DefaultEffectPrefab != null)
                {
                    effectManager.Comp.StartEffectsWithRandomPosRot(DefaultEffectPrefab, Range, size, isFollow);
                }
                return;
            }

            effectManager.Comp.StartEffectsWithRandomPosRot(EffectPrefab, Range, size, isFollow);
        }

        /// <summary>
        /// Random위치에 파티클을 생성하고 UI상으로 같은 위치에 DamageText를 생성하는 로직
        /// </summary>
        /// <param name="effectPrefab">생성할 파티클</param>
        /// <param name="range">피격위치로부터 랜덤 위치값을 가져올 구 범위의 반지름</param>
        /// <param name="damage">피격 데미지</param>
        /// <param name="fontSize">DamageText 폰트 사이즈</param>
        /// <param name="damageAttiribute">Damage속성</param>
        /// <returns></returns>
        public GameObject HUD_DmgTxt(GameObject effectPrefab, float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute, bool isCritical = false)
        {
            var effect = effectManager.Comp?.StartEffectsWithRandomPos(effectPrefab, range);

            var pos = new Vector2((Camera.main.WorldToViewportPoint(transform.position).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(transform.position).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f));
            GameObject damageText;

            if (isCritical)
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.CriticalDamageTextPrefab, Vector3.one) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            else
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.DamageTextPrefab, Vector3.one) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            return damageText;
        }

        public GameObject HUD_DmgTxt(float damage, float fontSize, DAMAGE_ATT damageAttiribute, bool isCritical = false)
        {
            var pos = new Vector2((Camera.main.WorldToViewportPoint(transform.position).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(transform.position).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f));
            GameObject damageText;

            if (isCritical)
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.CriticalDamageTextPrefab, Vector3.one) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            else
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.DamageTextPrefab, Vector3.one) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            return damageText;
        }

        public GameObject HUD_DmgTxt(float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute)
        {
            return HUD_DmgTxt(range, damage, fontSize, damageAttiribute, false);
        }
        public GameObject HUD_DmgTxt(float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute, bool isCritical = false)
        {
            var randomPos = new Vector2(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range));

            var pos = new Vector2((Camera.main.WorldToViewportPoint(randomPos).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(randomPos).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f));

            GameObject damageText;

            if (isCritical)
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.CriticalDamageTextPrefab, Vector3.one) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
                if (GlobalValue.CriticalHit_SFX != null)
                    GameManager.Inst.StageManager?.player?.Core.CoreSoundEffect.AudioSpawn(GlobalValue.CriticalHit_SFX);
            }
            else
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.DamageTextPrefab, Vector3.one) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            return damageText;
        }

        protected override void Awake()
        {
            base.Awake();
            BC2D = GetComponent<BoxCollider2D>();
            stats = new CoreComp<UnitStats>(core);
            effectManager = new CoreComp<EffectManager>(core);
            death = new CoreComp<Death>(core);
            this.tag = core.Unit.gameObject.tag;
        }
    }
}