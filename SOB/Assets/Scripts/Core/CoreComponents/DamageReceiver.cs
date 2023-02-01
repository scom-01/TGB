using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        private CoreComp<UnitStats> stats;
        private CoreComp<ParticleManager> particleManager;
        public void Damage(GameObject attacker, GameObject victim, ElementalPower elementalPower, DamageAttiribute attiribute, float amount)
        {
            //attacker.GetComponentInChildren<Core>().GetCoreComponent<UnitStats>()
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            stats.Comp?.DecreaseHealth(elementalPower, attiribute, amount);            
            //if (damageParticles == null)
            //{
            //    Debug.Log("Combat DamageParticles is Null");
            //    return;
            //}
            //particleManager.Comp?.StartParticlesWithRandomRotation(damageParticles);
        }
        public void Damage(CommonData AttackterCommonData, CommonData VictimCommonData, GameObject EffectPrefab, float amount)
        {
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
            
            //TODO: 계산한 데미지 수치 return 필요
            stats.Comp?.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            if (EffectPrefab == null)
            {
                Debug.Log("Combat DamageParticles is Null");
                return;
            }

            //간결화 필요
            var particle = particleManager.Comp?.StartParticlesWithRandomPosition(EffectPrefab, 1.5f);

            var pos = new Vector2((Camera.main.WorldToViewportPoint(particle.transform.position).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(particle.transform.position).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 50);
            var damageText = Instantiate(GameManager.Inst.player.GetComponent<Player>().DamageTextPrefab,
                            new Vector3(pos.x, pos.y),
                            Quaternion.identity, GameManager.Inst.DamageUI.transform);
            //간결화 필요

            damageText.GetComponent<RectTransform>().anchoredPosition = pos;
            damageText.GetComponentInChildren<DamageText>().HitTextMeshPro.text = amount.ToString();
            damageText.GetComponentInChildren<DamageText>().Color = Color.black;
            damageText.GetComponentInChildren<DamageText>().FontSize = 50;

        }

        protected override void Awake()
        {
            base.Awake();

            stats = new CoreComp<UnitStats>(core);
            particleManager = new CoreComp<ParticleManager>(core);
        }
    }
}