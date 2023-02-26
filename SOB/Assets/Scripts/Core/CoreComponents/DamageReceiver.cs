using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        public GameObject DefaultEffectPrefab;

        private CoreComp<UnitStats> stats;
        private CoreComp<ParticleManager> particleManager;

        public bool isHit = false;
        public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, GameObject EffectPrefab, float amount)
        {
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            
            //TODO: 계산한 데미지 수치 return 필요
            //stats.Comp?.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            isHit = true;
            if (EffectPrefab == null)
            {
                Debug.Log("Combat DamageParticles is Null");
                return;
            }

            RandomParticleInstantiate(EffectPrefab, 1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
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
        public GameObject RandomParticleInstantiate(GameObject effectPrefab, float range, float damage, float fontSize, DamageAttiribute damageAttiribute)
        {
            var particle = particleManager.Comp?.StartParticlesWithRandomPosition(effectPrefab, range);

            var pos = new Vector2((Camera.main.WorldToViewportPoint(particle.transform.position).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(particle.transform.position).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 50);   //50 = DamageText의 높이
            var damageText = Instantiate(GameManager.Inst.player.GetComponent<Player>().DamageTextPrefab,
                            new Vector3(pos.x, pos.y),
                            Quaternion.identity, GameManager.Inst.DamageUI.transform);

            damageText.GetComponent<RectTransform>().anchoredPosition = pos;
            switch (damageAttiribute)
            {
                case DamageAttiribute.Magic:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.magenta);
                    break;
                case DamageAttiribute.Physics:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.yellow);
                    break;
                case DamageAttiribute.Fixed:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.black);
                    break;
            }

            return damageText;
        }

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<UnitStats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}