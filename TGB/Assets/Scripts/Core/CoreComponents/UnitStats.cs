using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace TGB.CoreSystem
{
    public class UnitStats : CoreComponent
    {
        public event Action OnHealthZero;
        public event Action OnChangeHealth;

        #region Stats       
        
        public StatsData StatsData { get => Default_statsData; set => Default_statsData = value; }
        [field: Header("Stats")]
        [field: SerializeField] private StatsData Default_statsData;
        public StatsData CalculStatsData
        {
            get
            {
                StatsData data = new StatsData()
                {
                    MaxHealth = MaxHealth,
                    DefaultPower = DefaultPower,
                    DefaultMoveSpeed = DefaultMoveSpeed,
                    DefaultJumpVelocity = DefaultJumpVelocity,
                    DefaultAttSpeed = DefaultAttSpeed,
                    MovementVEL_Per = MoveSpeed,
                    JumpVEL_Per = JumpVelocity,
                    AttackSpeedPer = AttackSpeedPer,
                    PhysicsDefensivePer = PhysicsDefensivePer,
                    MagicDefensivePer = MagicDefensivePer,
                    PhysicsAggressivePer = PhysicsAggressivePer,
                    MagicAggressivePer = MagicAggressivePer,
                    CriticalPer = CriticalPer,
                    AdditionalCriticalPer = AdditionalCriticalPer,
                    DamageAttiribute = DamageAttiribute,
                    Elemental = Elemental,
                    ElementalDefensivePer = ElementalDefensivePer,
                    ElementalAggressivePer = ElementalAggressivePer,
                };
                return data;
            }
            set => m_statsData = value;
        }
        public StatsData m_statsData;

        public BlessStatsData BlessStats;

        public float invincibleTime;
        public float TouchinvincibleTime;
        public float CurrentHealth
        {
            get => currentHealth;
            private set
            {
                currentHealth = value <= 0 ? 0 : (value >= Default_statsData.MaxHealth + m_statsData.MaxHealth ? Default_statsData.MaxHealth : value);
                OnChangeHealth?.Invoke();
            }
        }
        [SerializeField] private float currentHealth;


        /// <summary>
        /// 물리 방어력 최대 100%의 피해 흡수
        /// </summary>
        public float PhysicsDefensivePer { get => Mathf.Clamp((Default_statsData.PhysicsDefensivePer + m_statsData.PhysicsDefensivePer + BlessStats.Bless_Def_Lv * GlobalValue.BlessingStats_Inflation), 0, 100.0f); }

        /// <summary>
        /// 마법 방어력 최대 100%의 피해 흡수
        /// </summary>
        public float MagicDefensivePer { get => Mathf.Clamp((Default_statsData.MagicDefensivePer + m_statsData.MagicDefensivePer + BlessStats.Bless_Def_Lv * GlobalValue.BlessingStats_Inflation), 0, 100.0f); }

        /// <summary>
        /// 공격력
        /// </summary>
        public float DefaultPower { get => Default_statsData.DefaultPower + m_statsData.DefaultPower; }

        /// <summary>
        /// 추가 물리공격력 %
        /// </summary>
        public float PhysicsAggressivePer { get => (Default_statsData.PhysicsAggressivePer + m_statsData.PhysicsAggressivePer + BlessStats.Bless_Agg_Lv * GlobalValue.BlessingStats_Inflation); }

        /// <summary>
        /// 추가 마법공격력 %
        /// </summary>
        public float MagicAggressivePer { get => (Default_statsData.MagicAggressivePer + m_statsData.MagicAggressivePer + BlessStats.Bless_Agg_Lv * GlobalValue.BlessingStats_Inflation); }

        /// <summary>
        /// 크리티컬 확률
        /// </summary>
        public float CriticalPer { get => Mathf.Clamp((Default_statsData.CriticalPer + m_statsData.CriticalPer + BlessStats.Bless_Critical_Lv * GlobalValue.BlessingStats_Inflation), 0, 100.0f); }

        /// <summary>
        /// 추가 크리티컬 데미지
        /// </summary>
        public float AdditionalCriticalPer { get => (Default_statsData.AdditionalCriticalPer + m_statsData.AdditionalCriticalPer + BlessStats.Bless_Critical_Lv * GlobalValue.BlessingStats_Inflation); }

        /// <summary>
        /// 원소 속성 (공격과 방어 모두에 적용)
        /// </summary>
        public E_Power Elemental { get => Default_statsData.Elemental; }

        /// <summary>
        /// 원소 저항력 (수치만큼 %로 감소)
        /// </summary>
        public float ElementalDefensivePer { get => Mathf.Clamp((Default_statsData.ElementalDefensivePer + m_statsData.ElementalDefensivePer + BlessStats.Bless_Elemental_Lv * GlobalValue.BlessingStats_Inflation), 0, 100.0f); }

        /// <summary>
        /// 원소 공격력 (수치만큼 %로 증가)
        /// </summary>
        public float ElementalAggressivePer { get => (Default_statsData.ElementalAggressivePer + m_statsData.ElementalAggressivePer + BlessStats.Bless_Elemental_Lv * GlobalValue.BlessingStats_Inflation); }
        /// <summary>
        /// 기본 공격 속도
        /// </summary>
        public float DefaultAttSpeed { get => Default_statsData.DefaultAttSpeed + m_statsData.DefaultAttSpeed; }
        /// <summary>
        /// 공격속도 (수치만큼 %로 증가)
        /// </summary>
        public float AttackSpeedPer { get => (Default_statsData.AttackSpeedPer + m_statsData.AttackSpeedPer + BlessStats.Bless_Speed_Lv * GlobalValue.BlessingStats_Inflation); }
        public float MaxHealth { get => Default_statsData.MaxHealth + m_statsData.MaxHealth; set => Default_statsData.MaxHealth = value; }
        /// <summary>
        /// 기본 이동속도
        /// </summary>
        public float DefaultMoveSpeed { get => Default_statsData.DefaultMoveSpeed + m_statsData.DefaultMoveSpeed; }
        /// <summary>
        /// 추가 이동속도(수치만큼 %로 증가)
        /// </summary>
        public float MoveSpeed { get => (Default_statsData.MovementVEL_Per + m_statsData.MovementVEL_Per + BlessStats.Bless_Speed_Lv * GlobalValue.BlessingStats_Inflation); }
        /// <summary>
        /// 기본 점프력
        /// </summary>
        public float DefaultJumpVelocity { get => Default_statsData.DefaultJumpVelocity + m_statsData.DefaultJumpVelocity; }
        /// <summary>
        /// 추가 점프력 (수치만큼 %로 증가)
        /// </summary>
        public float JumpVelocity { get => Default_statsData.JumpVEL_Per + m_statsData.JumpVEL_Per; }

        /// <summary>
        /// 공격 속성 
        /// </summary>
        public DAMAGE_ATT DamageAttiribute { get => Default_statsData.DamageAttiribute; set => Default_statsData.DamageAttiribute = value; }
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

        public void SetHealth(float amount)
        {
            CurrentHealth = amount;
        }

        public float IncreaseHealth(float amount)
        {
            var oldHealth = CurrentHealth;
            CurrentHealth += amount;

            core.CoreDamageReceiver.HUD_DmgTxt(1.0f, CurrentHealth - oldHealth, 30, DAMAGE_ATT.Heal, false);

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
            CurrentHealth -= amount2;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount2;
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
            float amount2 = CalculateDamageAtt(AttackerData, VictimData, VictimData.DamageAttiribute, amount1);
            #endregion

            return amount2;
        }

        public float CalculateElementDamage(StatsData AttackerData, StatsData VictimData, E_Power e_Power, float amount)
        {
            Debug.Log($"Before Calculator ElementalPower = {amount}");

            //Normal이 아닌 속성을 보유하고있을 때
            if(e_Power != E_Power.Normal)
            {
                amount *= (1.0f + AttackerData.ElementalAggressivePer / 100f);
            }
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
                        amount *= (1.0f - GlobalValue.E_WeakPer * (1.0f - VictimData.ElementalDefensivePer / 100));
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