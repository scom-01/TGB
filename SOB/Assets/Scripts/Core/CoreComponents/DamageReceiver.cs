using Unity.VisualScripting;
using UnityEngine;

namespace SOB.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        public GameObject DefaultEffectPrefab;

        private CoreComp<UnitStats> stats;
        private CoreComp<EffectManager> effectManager;
        private CoreComp<Death> death;
        private BoxCollider2D BC2D;

        public bool isHit
        {
            get => ishit;
            set
            {
                BC2D.enabled = !value;
                ishit = value;
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
            if(death.Comp.isDead)
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

            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, VictimCommonData, amount);
            isHit = true;
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            RandomEffectInstantiate(1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
        }
        public void Damage(StatsData AttackterCommonData, float amount)
        {
            if(death.Comp.isDead)
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

            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, amount);
            isHit = true;
            stats.Comp.invincibleTime = core.Unit.UnitData.invincibleTime;
            RandomEffectInstantiate(1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
        }

        public void TrapDamage(StatsData AttackterCommonData, float amount)
        {
            if(death.Comp.isDead)
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
            var damage = stats.Comp.DecreaseHealth(AttackterCommonData, amount);            
            stats.Comp.TouchinvincibleTime = core.Unit.UnitData.touchDamageinvincibleTime;

            RandomEffectInstantiate(1.0f, damage, 50, AttackterCommonData.DamageAttiribute);
        }
        public void HitAction(GameObject EffectPrefab, float Range, int FancingDirection)
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
        public GameObject RandomEffectInstantiate(GameObject effectPrefab, float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute)
        {
            var effect = effectManager.Comp?.StartEffectsWithRandomPos(effectPrefab, range);

            var pos = new Vector2((Camera.main.WorldToViewportPoint(effect.transform.position).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(effect.transform.position).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 50);   //50 = DamageText의 높이

            var damageText = Instantiate(GameManager.Inst.StageManager.player.GetComponent<Player>().DamageTextPrefab,
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

        public GameObject RandomEffectInstantiate(float range, float damage, float fontSize, DAMAGE_ATT damageAttiribute)
        {
            var randomPos = new Vector2(transform.position.x + Random.Range(-range, range), transform.position.y + Random.Range(-range, range));

            var pos = new Vector2((Camera.main.WorldToViewportPoint(randomPos).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
                                    (Camera.main.WorldToViewportPoint(randomPos).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f) + 50);   //50 = DamageText의 높이
            var damageText = Instantiate(GameManager.Inst.StageManager.player.GetComponent<Player>().DamageTextPrefab,
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
            BC2D = GetComponent<BoxCollider2D>();
            stats = new CoreComp<UnitStats>(core);
            effectManager = new CoreComp<EffectManager>(core);
            death = new CoreComp<Death>(core);
            this.tag = core.Unit.gameObject.tag;
        }
    }
}