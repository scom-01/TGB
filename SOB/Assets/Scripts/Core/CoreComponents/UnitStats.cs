using System;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class UnitStats : CoreComponent
    {
        public event Action OnHealthZero;
        public event Action OnChangeHealth;

        #region Stats        
        public StatsData StatsData { get => statsData; set => statsData = value; }
        [field: SerializeField] private StatsData statsData;

        public float invincibleTime;
        public float TouchinvincibleTime;
        public float CurrentHealth 
        { 
            get => currentHealth; 
            set
            {                
                currentHealth = value <= 0 ? 0 : (value >= statsData.MaxHealth ? statsData.MaxHealth : value);
                OnChangeHealth?.Invoke();
            }
        }
        [SerializeField] private float currentHealth;

        /// <summary>
        /// 물리 방어력 최대 100%의 피해 흡수
        /// </summary>
        public float PhysicsDefensivePer { get => statsData.PhysicsDefensivePer; set => statsData.PhysicsDefensivePer = Mathf.Clamp(value, 0, 100.0f); }

        /// <summary>
        /// 마법 방어력 최대 100%의 피해 흡수
        /// </summary>
        public float MagicDefensivePer { get => statsData.MagicDefensivePer; set => statsData.MagicDefensivePer = Mathf.Clamp(value, 0, 100.0f); }

        /// <summary>
        /// 공격력
        /// </summary>
        public float DefaultPower { get => statsData.DefaultPower; set => statsData.DefaultPower = value <= 0 ? 0 : value; }

        /// <summary>
        /// 추가 물리공격력 %
        /// </summary>
        public float PhysicsAggressivePer { get => statsData.PhysicsAggressivePer; set => statsData.PhysicsAggressivePer = value <= 0 ? 0 : value; }

        /// <summary>
        /// 추가 마법공격력 %
        /// </summary>
        public float MagicAggressivePer { get => statsData.MagicAggressivePer; set => statsData.MagicAggressivePer = value <= 0 ? 0 : value; }

        /// <summary>
        /// 크리티컬 확률
        /// </summary>
        public float CriticalPer { get=>statsData.CriticalPer; set => statsData.CriticalPer = Mathf.Clamp(value, 0, 100.0f); }

        /// <summary>
        /// 추가 크리티컬 데미지
        /// </summary>
        public float AdditionalCriticalPer { get => statsData.AdditionalCriticalPer; set => statsData.AdditionalCriticalPer = value <= 0 ? 0 : value; }
        
        /// <summary>
        /// 원소 속성 (공격과 방어 모두에 적용)
        /// </summary>
        public E_Power Elemental { get => statsData.Elemental; set => statsData.Elemental = value; }

        /// <summary>
        /// 원소 저항력 (수치만큼 %로 감소)
        /// </summary>
        public float ElementalDefensivePer { get => statsData.ElementalDefensivePer; set => statsData.ElementalDefensivePer = Mathf.Clamp(value, 0, 100.0f); }

        /// <summary>
        /// 원소 공격력 (수치만큼 %로 증가)
        /// </summary>
        public float ElementalAggressivePer { get => statsData.ElementalAggressivePer; set => statsData.ElementalAggressivePer = value; }
        /// <summary>
        /// 원소 공격력 (수치만큼 %로 증가)
        /// </summary>
        public float AttackSpeedPer { get => statsData.AttackSpeedPer; set => statsData.AttackSpeedPer = value; }
        public float MaxHealth { get => statsData.MaxHealth; set => statsData.MaxHealth = value; }
        public float MoveSpeed { get => statsData.MovementVelocity; set => statsData.MovementVelocity = value; }
        public float JumpVelocity { get => statsData.JumpVelocity; set => statsData.JumpVelocity = value; }

        /// <summary>
        /// 공격 속성 
        /// </summary>
        public DAMAGE_ATT DamageAttiribute { get => statsData.DamageAttiribute; set => statsData.DamageAttiribute = value; }
        #endregion
        private bool isSetup = false;

        protected override void Awake()
        {
            base.Awake();
            isSetup = false;
            if (core.Unit.UnitData != null && !isSetup)
            {
                StatsData = core.Unit.UnitData.statsStats;
                invincibleTime = core.Unit.UnitData.invincibleTime;
                TouchinvincibleTime = core.Unit.UnitData.touchDamageinvincibleTime;
                CurrentHealth = MaxHealth;
            }
        }

        public float IncreaseHealth(float amount)
        {
            var oldHealth = CurrentHealth;
            CurrentHealth += amount;

            //증가한 체력량
            return CurrentHealth - oldHealth;
        }

        /// <summary>
        /// 공격하는 주체가 명확하지 않은 경우(낙사, 트랩 등)
        /// </summary>
        /// <param name="elemental"></param>
        /// <param name="attiribute"></param>
        /// <param name="amount"></param>
        public float DecreaseHealth(E_Power elemental, DAMAGE_ATT attiribute, float amount)
        {
            core.Unit.HitEffect();
            //CriticalPer
            float amount1 = CalculateElementDamage(elemental, amount);
            float amount2 = CalculateDamageAtt(attiribute, amount1);            
            
            CurrentHealth -= amount2;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount2;
        }

        public float DecreaseHealth(StatsData AttackerData, StatsData VictimData, E_Power _elemental, DAMAGE_ATT attiribute, float amount)
        {
            core.Unit.HitEffect();
            float amount1 = CalculateElementDamage(AttackerData, VictimData, _elemental, amount);
            float amount2 = CalculateDamageAtt(AttackerData, VictimData, AttackerData.DamageAttiribute, amount1);
            CurrentHealth -= amount;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount;
        }

        //공격자의 보유 Elemental과 다른 Elemental속성을 공격할 때
        public float DecreaseHealth(StatsData AttackerData, StatsData VictimData, E_Power _elemental, float amount)
        {
            core.Unit.HitEffect();
            
            float amount1 = CalculateElementDamage(AttackerData, VictimData, _elemental, amount);
            float amount2 = CalculateDamageAtt(AttackerData, VictimData, AttackerData.DamageAttiribute, amount1);
            CurrentHealth -= amount2;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount2;
        }

        public float DecreaseHealth(float amount)
        {
            core.Unit.HitEffect();
            CurrentHealth -= amount;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount;
        }

        public float CalculateDamage(StatsData AttackerData, StatsData VictimData, float amount)
        {
            #region 원소속성 계산
            float amount1 = CalculateElementDamage(AttackerData, VictimData, AttackerData.Elemental, amount);
            #endregion

            #region 속성 계산
            float amount2 = CalculateDamageAtt( AttackerData, VictimData, VictimData.DamageAttiribute, amount1);
            #endregion

            return amount2;
        }

        public float CalculateElementDamage(StatsData AttackerData, StatsData VictimData, E_Power e_Power, float amount)
        {
            Debug.Log($"Before Calculator ElementalPower = {amount}");

            amount *= (1.0f + AttackerData.ElementalAggressivePer / 100f);
            //Water(4) > Earth(3) > Wind(2) > Fire(1) > Water
            if ((int)AttackerData.Elemental == (int)e_Power)
            {
                Debug.Log($"ElementalPower is the  same {VictimData.Elemental}! Not Increase and Not Decrease");
            }
            else
            {
                if ((int)AttackerData.Elemental > (int)e_Power)
                {
                    if ((int)AttackerData.Elemental == 4 && (int)e_Power == 1)
                    {
                        amount *= (1.0f - GlobalValue.E_WeakPer * (1.0f - VictimData.ElementalDefensivePer/ 100));
                    }
                    else
                    {
                        amount *= (1.0f + GlobalValue.E_WeakPer * (1.0f - VictimData.ElementalDefensivePer / 100));
                    }
                }
                else if ((int)AttackerData.Elemental < (int)e_Power)
                {
                    if ((int)AttackerData.Elemental == 1 && (int)e_Power == 4)
                    {
                        amount *= (1.0f - GlobalValue.E_WeakPer * (1.0f - VictimData.ElementalDefensivePer / 100));
                    }
                    else
                    {
                        amount *= (1.0f + GlobalValue.E_WeakPer * (1.0f - VictimData.ElementalDefensivePer / 100));
                    }
                }
                //elemental == MyElemental 같거나 Normal일때
                else
                {
                    Debug.Log($"{core.transform.parent.name}의 MyElemental 과 받는 ElememtalPower가 같음! Elemental 증가 및 감소 없음");
                }
            }
            Debug.Log($"After Calculator ElementalPower = {amount}");
            return amount;
        }

        public float CalculateElementDamage(E_Power e_Power, float amount)
        {
            Debug.Log($"Before Calculator ElementalPower = {amount}");

            //Water(4) > Earth(3) > Wind(2) > Fire(1) > Water
            if ((int)e_Power == (int)Elemental)
            {
                Debug.Log($"ElementalPower is the  same {e_Power}! Not Increase and Not Decrease");
            }
            else
            {
                if ((int)e_Power > (int)Elemental)
                {
                    if ((int)e_Power == 4 && (int)e_Power == 1)
                    {
                        amount *= (1.0f - GlobalValue.E_WeakPer);
                    }
                    else
                    {
                        amount *= (1.0f + GlobalValue.E_WeakPer);
                    }
                }
                else if ((int)e_Power < (int)e_Power)
                {
                    if ((int)e_Power == 1 && (int)e_Power == 4)
                    {
                        amount *= (1.0f - GlobalValue.E_WeakPer);
                    }
                    else
                    {
                        amount *= (1.0f + GlobalValue.E_WeakPer);
                    }
                }
                //elemental == MyElemental 같거나 Normal일때
                else
                {
                    Debug.Log($"{core.transform.parent.name}의 MyElemental 과 받는 ElememtalPower가 같음! Elemental 증가 및 감소 없음");
                }
            }
            Debug.Log($"After Calculator ElementalPower = {amount}");
            return amount;
        }

        public float CalculateDamageAtt(StatsData AttackerData, StatsData VictimData, DAMAGE_ATT Damage_att, float amount)
        {
            Debug.Log($"Before Calculator DamageAttribute = {amount}");
            switch (Damage_att)
            {
                case DAMAGE_ATT.Physics:
                    amount *= (1.0f + AttackerData.PhysicsAggressivePer / 100);
                    amount *= (1.0f - VictimData.PhysicsDefensivePer / 100);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DAMAGE_ATT.Magic:
                    amount *= (1.0f + AttackerData.MagicAggressivePer / 100);
                    amount *= (1.0f - VictimData.MagicDefensivePer / 100);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DAMAGE_ATT.Fixed:
                    //고정 데미지 일 시 감소 없음
                    break;
            }
            Debug.Log($"Atfer Calculator DamageAttribute = {amount}");
            return amount;
        }
        public float CalculateDamageAtt(DAMAGE_ATT Damage_att, float amount)
        {
            Debug.Log($"Before Calculator DamageAttribute = {amount}");
            switch (Damage_att)
            {
                case DAMAGE_ATT.Physics:
                    amount *= (1.0f - PhysicsDefensivePer / 100);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DAMAGE_ATT.Magic:
                    amount *= (1.0f - MagicDefensivePer / 100);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DAMAGE_ATT.Fixed:
                    //고정 데미지 일 시 감소 없음
                    break;
            }
            Debug.Log($"Atfer Calculator DamageAttribute = {amount}");
            return amount;
        }
        public void SetStat(StatsData _statData, float _currentHealth)
        {
            StatsData = _statData;
            CurrentHealth = _currentHealth;
            isSetup = true;
        }
    }
}