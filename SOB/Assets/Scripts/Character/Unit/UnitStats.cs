using SOB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class UnitStats : CoreComponent
    {
        public event Action OnHealthZero;

        #region Stats        
        public StatsData StatsData { get => statsData; set => statsData = value; }
        [field: SerializeField] private StatsData statsData;

        public float invincibleTime;
        public float CurrentHealth { get => currentHealth; set => currentHealth = value <= 0 ? 0 : value; }
        [SerializeField] private float currentHealth;

        /// <summary>
        /// 물리 방어력 최대 100%의 피해 흡수
        /// </summary>
        public float PhysicsDefensivePer { get => statsData.PhysicsDefensivePer; set => statsData.PhysicsDefensivePer = Mathf.Clamp(value, 0, 1.0f); }

        /// <summary>
        /// 마법 방어력 최대 100%의 피해 흡수
        /// </summary>
        public float MagicDefensivePer { get => statsData.MagicDefensivePer; set => statsData.MagicDefensivePer = Mathf.Clamp(value, 0, 1.0f); }

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
        /// 원소 속성 (공격과 방어 모두에 적용)
        /// </summary>
        public ElementalPower MyElemental { get => statsData.MyElemental; set => statsData.MyElemental = value; }        

        /// <summary>
        /// 원소 저항력 (수치만큼 %로 감소)
        /// </summary>
        public float ElementalDefensivePer { get => statsData.ElementalDefensivePer; set => statsData.ElementalDefensivePer = value; }

        /// <summary>
        /// 원소 공격력 (수치만큼 %로 증가)
        /// </summary>
        public float ElementalAggressivePer { get => statsData.ElementalAggressivePer; set => statsData.ElementalAggressivePer = value; }

        /// <summary>
        /// 공격 속성 
        /// </summary>
        public DamageAttiribute DamageAttiribute { get => statsData.DamageAttiribute; set => statsData.DamageAttiribute = value; }
        #endregion

        [SerializeField]
        private GameObject
            deathChunkParticle,
            deathBloodParticle;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            if(core.Unit.UnitData != null)
            {
                StatsData = core.Unit.UnitData.statsStats;
                invincibleTime = core.Unit.UnitData.invincibleTime;
                CurrentHealth = StatsData.MaxHealth;
            }            
        }

        public void Increase(string statName, float amount)
        {
            switch (statName)
            {
                case "MaxHealth":
                    statsData.MaxHealth += amount;
                    //현재 체력도 증가
                    currentHealth += amount;
                    break;
                case "currentHealth":
                    if (CurrentHealth + amount >= statsData.MaxHealth)
                    {
                        CurrentHealth = statsData.MaxHealth;
                    }
                    else
                    {
                        CurrentHealth += amount;
                    }
                    break;
                case "MovementVelocity":
                    statsData.MovementVelocity += amount;
                    break;
                case "DefaulPower":
                    statsData.DefaultPower += amount;
                    break;
                case "AttackSpeedPer":
                    statsData.AttackSpeedPer += amount;
                    break;
                case "PhysicsDefensivePer":
                    statsData.PhysicsDefensivePer += amount;
                    break;
                case "MagicDefensivePer":
                    statsData.MagicDefensivePer += amount;
                    break;
                case "PhysicsAggressivePer":
                    statsData.PhysicsAggressivePer += amount;
                    break;
                case "MagicAggressivePer":
                    statsData.MagicAggressivePer += amount;
                    break;
                case "ElementalDefensivePer":
                    statsData.ElementalDefensivePer += amount;
                    break;
                case "ElementalAggressivePer":
                    statsData.ElementalAggressivePer += amount;
                    break;
            }   

        }
        public void Decrease(string statName, float amount)
        {
            switch (statName)
            {
                case "MaxHealth":
                    statsData.MaxHealth -= amount;
                    //현재 체력도 감소
                    CurrentHealth -= amount;
                    break;
                case "currentHealth":
                    CurrentHealth -= amount;
                    break;
                case "MovementVelocity":
                    statsData.MovementVelocity -= amount;
                    break;
                case "DefaulPower":
                    statsData.DefaultPower -= amount;
                    break;
                case "AttackSpeedPer":
                    statsData.AttackSpeedPer -= amount;
                    break;
                case "PhysicsDefensivePer":
                    statsData.PhysicsDefensivePer -= amount;
                    break;
                case "MagicDefensivePer":
                    statsData.MagicDefensivePer -= amount;
                    break;
                case "PhysicsAggressivePer":
                    statsData.PhysicsAggressivePer -= amount;
                    break;
                case "MagicAggressivePer":
                    statsData.MagicAggressivePer -= amount;
                    break;
                case "ElementalDefensivePer":
                    statsData.ElementalDefensivePer -= amount;
                    break;
                case "ElementalAggressivePer":
                    statsData.ElementalAggressivePer -= amount;
                    break;
            }   

        }

        /// <summary>
        /// 공격하는 주체가 명확하지 않은 경우(낙사, 트랩 등)
        /// </summary>
        /// <param name="elemental"></param>
        /// <param name="attiribute"></param>
        /// <param name="amount"></param>
        public float DecreaseHealth(ElementalPower elemental, DamageAttiribute attiribute, float amount)
        {
            CalculateDamage(elemental, attiribute, amount);
            CurrentHealth -= amount;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount;
        }

        public float DecreaseHealth(StatsData AttackerData, StatsData VictimData, float amount)
        {
            amount = CalculateDamage(AttackerData, VictimData, amount);
            CurrentHealth -= amount;

            Debug.Log($"{core.transform.parent.name} Health = {currentHealth}");
            if (CurrentHealth == 0.0f)
            {
                OnHealthZero?.Invoke();
            }
            return amount;
        }

        public float CalculateDamage(ElementalPower elemental, DamageAttiribute attiribute, float amount)
        {
            #region 원소속성 계산
            Debug.Log($"Before Calculator ElementalPower = {amount}");

            //Water(4) > Earth(3) > Wind(2) > Fire(1) > Water
            if ((int)elemental == (int)ElementalPower.Normal)
            {
                Debug.Log($"ElementalPower is Normal! Not Increase and Not Decrease");
            }
            else
            {
                if ((int)elemental > (int)MyElemental)
                {
                    //0.7f, 1.3f또한 변수화 예정
                    if ((int)elemental == 4 && (int)MyElemental == 1)
                    {
                        amount *= (0.7f * (1.0f - ElementalDefensivePer));
                    }
                    else
                    {
                        amount *= (1.3f * (1.0f - ElementalDefensivePer));
                    }
                }
                else if ((int)elemental < (int)MyElemental)
                {
                    if ((int)elemental == 1 && (int)MyElemental == 4)
                    {
                        amount *= (0.7f * (1.0f - ElementalDefensivePer));
                    }
                    else
                    {
                        amount *= (1.3f * (1.0f - ElementalDefensivePer));
                    }
                }
                //elemental == MyElemental 같거나 Normal일때
                else
                {
                    Debug.Log($"{core.transform.parent.name}의 MyElemental 과 받는 ElememtalPower가 같음! Elemental 증가 및 감소 없음");
                }
            }
            Debug.Log($"After Calculator ElementalPower = {amount}");
            #endregion

            #region 속성 계산
            Debug.Log($"Before Calculator DamageAttribute = {amount}");
            switch (attiribute)
            {
                case DamageAttiribute.Physics:
                    amount *= (1.0f - PhysicsDefensivePer);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DamageAttiribute.Magic:
                    amount *= (1.0f - MagicDefensivePer);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DamageAttiribute.Fixed:
                    //고정 데미지 일 시 감소 없음
                    break;
            }
            Debug.Log($"Atfer Calculator DamageAttribute = {amount}");
            #endregion
            return amount;
        }
        public float CalculateDamage(StatsData AttackerData, StatsData VictimData, float amount)
        {
            #region 원소속성 계산
            Debug.Log($"Before Calculator ElementalPower = {amount}");

            amount *= (1.0f + AttackerData.ElementalAggressivePer);
            //Water(4) > Earth(3) > Wind(2) > Fire(1) > Water
            if ((int)AttackerData.MyElemental == (int)VictimData.MyElemental)
            {
                Debug.Log($"ElementalPower is Normal! Not Increase and Not Decrease");
            }
            else
            {
                if ((int)AttackerData.MyElemental > (int)VictimData.MyElemental)
                {
                    //0.7f, 1.3f또한 변수화 예정
                    if ((int)AttackerData.MyElemental == 4 && (int)VictimData.MyElemental == 1)
                    {
                        amount *= (0.7f * (1.0f - VictimData.ElementalDefensivePer));
                    }
                    else
                    {
                        amount *= (1.3f * (1.0f - VictimData.ElementalDefensivePer));
                    }
                }
                else if ((int)AttackerData.MyElemental < (int)VictimData.MyElemental)
                {
                    if ((int)AttackerData.MyElemental == 1 && (int)VictimData.MyElemental == 4)
                    {
                        amount *= (0.7f * (1.0f - VictimData.ElementalDefensivePer));
                    }
                    else
                    {
                        amount *= (1.3f * (1.0f - VictimData.ElementalDefensivePer));
                    }
                }
                //elemental == MyElemental 같거나 Normal일때
                else
                {
                    Debug.Log($"{core.transform.parent.name}의 MyElemental 과 받는 ElememtalPower가 같음! Elemental 증가 및 감소 없음");
                }
            }
            Debug.Log($"After Calculator ElementalPower = {amount}");
            #endregion

            #region 속성 계산
            Debug.Log($"Before Calculator DamageAttribute = {amount}");            
            switch (VictimData.DamageAttiribute)
            {
                case DamageAttiribute.Physics:
                    amount *= (1.0f + AttackerData.PhysicsAggressivePer);
                    amount *= (1.0f - VictimData.PhysicsDefensivePer);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DamageAttiribute.Magic:
                    amount *= (1.0f + AttackerData.MagicAggressivePer);
                    amount *= (1.0f - VictimData.MagicDefensivePer);
                    if (amount <= 0.0f)
                        return 0;
                    break;
                case DamageAttiribute.Fixed:
                    //고정 데미지는 Physics와 Magic AggressivePer 합의 곱
                    amount *= (1.0f + AttackerData.PhysicsAggressivePer + AttackerData.MagicAggressivePer);
                    //고정 데미지 일 시 감소 없음
                    break;
            }
            Debug.Log($"Atfer Calculator DamageAttribute = {amount}");
            #endregion
            return amount;
        }

    }
}