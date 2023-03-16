using UnityEngine;

namespace SOB.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        public GameObject DefaultEffectPrefab;

        private CoreComp<UnitStats> stats;
        private CoreComp<ParticleManager> particleManager;

        public bool isHit = false;
        public void Damage(StatsData AttackterCommonData, StatsData VictimCommonData, float amount)
        {
            Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");

            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            isHit = true;

            RandomParticleInstantiate(1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
        }
        public void HitAction(GameObject EffectPrefab, float Range)
        {
            if (EffectPrefab == null)
            {
                if (DefaultEffectPrefab != null)
                    particleManager.Comp.StartParticlesWithRandomPos(DefaultEffectPrefab, Range);
                return;
            }
                
            particleManager.Comp.StartParticlesWithRandomPos(EffectPrefab, Range);
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
        public GameObject RandomParticleInstantiate(GameObject effectPrefab, float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute)
        {
            var particle = particleManager.Comp?.StartParticlesWithRandomPos(effectPrefab, range);

            var pos = new Vector2((Camera.main.WorldToViewportPoint(particle.transform.position).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(particle.transform.position).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 50);   //50 = DamageText의 높이
            var damageText = Instantiate(GameManager.Inst.player.GetComponent<Player>().DamageTextPrefab,
                            new Vector3(pos.x, pos.y),
                            Quaternion.identity, GameManager.Inst.DamageUI.transform);

            damageText.GetComponent<RectTransform>().anchoredPosition = pos;
            switch (damageAttiribute)
            {
                case DAMAGE_ATT.Magic:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.magenta);
                    break;
                case DAMAGE_ATT.Physics:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.yellow);
                    break;
                case DAMAGE_ATT.Fixed:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.black);
                    break;
            }

            return damageText;
        }

        public GameObject RandomParticleInstantiate(float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute)
        {
            var randomPos = new Vector2(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range));

            var pos = new Vector2((Camera.main.WorldToViewportPoint(randomPos).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(randomPos).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 50);   //50 = DamageText의 높이
            var damageText = Instantiate(GameManager.Inst.player.GetComponent<Player>().DamageTextPrefab,
                            new Vector3(pos.x, pos.y),
                            Quaternion.identity, GameManager.Inst.DamageUI.transform);

            damageText.GetComponent<RectTransform>().anchoredPosition = pos;
            switch (damageAttiribute)
            {
                case DAMAGE_ATT.Magic:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.magenta);
                    break;
                case DAMAGE_ATT.Physics:
                    damageText.GetComponentInChildren<DamageText>().SetText(damage, fontSize, Color.yellow);
                    break;
                case DAMAGE_ATT.Fixed:
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