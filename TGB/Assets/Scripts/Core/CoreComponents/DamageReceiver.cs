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

        public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount)
        {
            Damage(AttackterCommonData, VictimCommonData, AttackterCommonData.Elemental, amount);
        }
        public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, E_Power _elemental, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }

            if (isHit)
            {
                Debug.Log(core.Unit.name + " isHit = true");
                return;
            }
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");

            bool isCritical = false;
            //크리티컬 계산
            if (AttackterCommonData.CriticalPer >= Random.Range(0, 100.0f))
            {
                isCritical = true;
                amount *= 1f + (AttackterCommonData.AdditionalCriticalPer / 100.0f);
            }

            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, VictimCommonData, _elemental, AttackterCommonData.DefaultPower + amount);
            isHit = true;
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            HUD_DmgTxt(1.0f, damage, 50, AttackterCommonData.DamageAttiribute, isCritical);
        }
        public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount, int repeat)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }

            if (isHit)
                return;

            for (int i = 0; i < repeat; i++)
            {
                TrueDamage(AttackterCommonData, VictimCommonData, amount);
            }
            isHit = true;
        }
        /// <summary>
        /// 히트 무적 무시
        /// </summary>
        /// <param name="AttackterCommonData"></param>
        /// <param name="VictimCommonData"></param>
        /// <param name="amount"></param>
        public void TrueDamage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount)
        {
            TrueDamage(AttackterCommonData, VictimCommonData, AttackterCommonData.Elemental, AttackterCommonData.DamageAttiribute, amount);
        }
        public void TrueDamage(StatsData AttackterCommonData, StatsData VictimCommonData, E_Power _Elemental, DAMAGE_ATT attribute, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }

            bool isCritical = false;
            //크리티컬 계산
            if (AttackterCommonData.CriticalPer >= Random.Range(0, 100.0f))
            {
                isCritical = true;
                amount *= 1f + (AttackterCommonData.AdditionalCriticalPer / 100.0f);
            }

            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, VictimCommonData, _Elemental, attribute, amount);
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            HUD_DmgTxt(1.0f, damage, 50, AttackterCommonData.DamageAttiribute, isCritical);
        }

        public void TypeCalDamage(Unit AttackerUnit, Unit VictimUnit, float AttackerDmg,int RepeatAmount = 1)
        {
            if (VictimUnit.gameObject.tag == "Player")
            {
                Damage
                    (
                    AttackerUnit.Core.CoreUnitStats.StatsData,
                    VictimUnit.Core.CoreUnitStats.CalculStatsData,
                    AttackerDmg,
                    RepeatAmount
                    );
            }
            else
            {
                //Damage
                switch ((VictimUnit as Enemy).enemyData.enemy_size)
                {
                    case ENEMY_Size.Small:
                        Damage
                        (
                        AttackerUnit.Core.CoreUnitStats.StatsData,
                        VictimUnit.Core.CoreUnitStats.CalculStatsData,
                        (AttackerDmg) * (1.0f + GlobalValue.Enemy_Size_WeakPer),
                        RepeatAmount
                        );
                        Debug.Log("Projectile Enemy Type Small, Normal Dam = " +
                            AttackerDmg
                            + " Enemy_Size_WeakPer Additional Dam = " +
                            (AttackerDmg) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                        break;
                    case ENEMY_Size.Medium:
                        Damage
                        (
                        AttackerUnit.Core.CoreUnitStats.StatsData,
                        VictimUnit.Core.CoreUnitStats.CalculStatsData,
                        (AttackerDmg), RepeatAmount
                        );
                        Debug.Log("Projectile Enemy Type Medium, Normal Dam = " +
                            AttackerUnit.Core.CoreUnitStats.DefaultPower);
                        break;
                    case ENEMY_Size.Big:
                        Damage
                        (   
                        AttackerUnit.Core.CoreUnitStats.StatsData,
                        VictimUnit.Core.CoreUnitStats.CalculStatsData,
                        (AttackerDmg) * (1.0f - GlobalValue.Enemy_Size_WeakPer),
                        RepeatAmount);

                        Debug.Log("Projectile Enemy Type Big, Normal Dam = " +
                            AttackerDmg
                            + " Enemy_Size_WeakPer Additional Dam = " +
                            (AttackerDmg) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                            );
                        break;
                }
            }
        }
        /// <summary>
        /// 고정 데미지
        /// </summary>
        /// <param name="amount">데미지 량</param>
        /// <param name="isTrueHit">피격 후 무적판정 무시공격(true)</param>
        /// <param name="RepeatAmount">반복 횟수(isTrueHit = false 때는 반영되지 않음)</param>
        public void FixedDamage(int amount, bool isTrueHit = false, int RepeatAmount = 1)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }

            if (isTrueHit)
            {
                Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
                var damage = stats.Comp.DecreaseHealth(amount);
                HUD_DmgTxt(1.0f, damage, 50, DAMAGE_ATT.Fixed, false);
                if(RepeatAmount > 1)
                {
                    var temp = RepeatAmount - 1;
                    FixedDamage(amount, isTrueHit, temp);
                    return;
                }
                stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            }
            else
            {
                if (isHit)
                {
                    Debug.Log(core.Unit.name + " isHit = true");
                    return;
                }
                Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
                var damage = stats.Comp.DecreaseHealth(amount);
                HUD_DmgTxt(1.0f, damage, 50, DAMAGE_ATT.Fixed, false);
                isHit = true;
                stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            }
        }
        public void TrapDamage(StatsData AttackterCommonData, float amount)
        {
            if (death.Comp.isDead)
            {
                Debug.Log(core.Unit.name + "is Dead");
                return;
            }
            if (isTouch)
            {
                return;
            }

            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            isTouch = true;
            var damage = stats.Comp.DecreaseHealth(AttackterCommonData.Elemental, AttackterCommonData.DamageAttiribute, amount);
            stats.Comp.TouchinvincibleTime = core.Unit.UnitData.touchDamageinvincibleTime;

            HUD_DmgTxt(1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
        }
        public void HitEffect(GameObject EffectPrefab, float Range, int FancingDirection)
        {
            if (EffectPrefab == null)
            {
                if (DefaultEffectPrefab != null)
                {
                    effectManager.Comp.StartEffectsWithRandomPos(DefaultEffectPrefab, Range, FancingDirection);
                }
                return;
            }

            effectManager.Comp.StartEffectsWithRandomPos(EffectPrefab, Range, FancingDirection);
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
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.CriticalDamageTextPrefab) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            else
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.DamageTextPrefab) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
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
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.CriticalDamageTextPrefab) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
            }
            else
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.DamageTextPrefab) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
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
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.CriticalDamageTextPrefab) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
                if (GlobalValue.CriticalHit_SFX != null)
                    GameManager.Inst.StageManager?.player?.Core.CoreSoundEffect.AudioSpawn(GlobalValue.CriticalHit_SFX);
            }
            else
            {
                damageText = (GameManager.Inst.StageManager.EffectContainer.CheckObject(ObjectPooling_TYPE.DmgText, GlobalValue.DamageTextPrefab) as DmgTxtPooling).GetObejct(new Vector3(pos.x, pos.y), Quaternion.identity, damage, fontSize, damageAttiribute);
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