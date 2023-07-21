using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.U2D.IK;

namespace SOB
{
    public class Projectile : MonoBehaviour
    {
        //공격하는 주체
        public Unit unit;
        public ProjectileData ProjectileData;
        private ProjectilePooling parent;

        [HideInInspector] public int FancingDirection = 1;
        private float m_startTime;
        public Rigidbody2D RB2D
        {
            get
            {
                if (rb2d == null)
                {
                    rb2d = this.GetComponent<Rigidbody2D>();
                    if (rb2d == null)
                        rb2d = this.AddComponent<Rigidbody2D>();
                    rb2d.gravityScale = ProjectileData.GravityScale;
                }
                return rb2d;
            }
        }
        private Rigidbody2D rb2d;

        private CircleCollider2D CC2D
        {
            get
            {
                if (cc2d == null)
                {
                    cc2d = this.GetComponent<CircleCollider2D>();
                    if (cc2d == null)
                        cc2d = this.AddComponent<CircleCollider2D>();
                    cc2d.isTrigger = true;
                }
                return cc2d;
            }
        }

        private CircleCollider2D cc2d;
        public Projectile(Unit _unit, ProjectileData m_ProjectileData)
        {
            unit = _unit;

            ProjectileData = m_ProjectileData;
            SetUp();
        }


        [ContextMenu("SetUp")]
        public void SetUp()
        {
            //Transform, Collider2D
            if (unit != null)
            {
                this.tag = unit.tag;
                this.transform.position = unit.transform.position + ProjectileData.Pos;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Projectile");
            this.transform.rotation = Quaternion.Euler(ProjectileData.Rot);


            CC2D.radius = ProjectileData.Radius;
            CC2D.enabled = false;
            RB2D.gravityScale = ProjectileData.GravityScale;

            //set ProjectilPrefab
            var _prefab = Instantiate(ProjectileData.ProjectilePrefab, this.transform);
            var main = _prefab.GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Disable;
            foreach (var _renderer in _prefab.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                _renderer.sortingLayerName = "Effect";
            }
            this.gameObject.SetActive(false);
        }

        public void SetUp(Unit _unit, ProjectileData m_ProjectileData)
        {
            unit = _unit;

            ProjectileData = m_ProjectileData;

            //Transform, Collider2D
            if (unit != null)
            {
                this.tag = unit.tag;
                this.transform.position = unit.transform.position + ProjectileData.Pos;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Projectile");
            this.transform.rotation = Quaternion.Euler(ProjectileData.Rot);


            CC2D.radius = ProjectileData.Radius;
            CC2D.enabled = false;
            RB2D.gravityScale = ProjectileData.GravityScale;
        }
        public void Init(ProjectileData m_ProjectileData)
        {
            ProjectileData = m_ProjectileData;
            SetUp();
        }

        public void Shoot()
        {
            //set startTime
            if (GameManager.Inst != null)
            {
                m_startTime = GameManager.Inst.PlayTime;
            }
            else
            {
                m_startTime = Time.time;
            }

            if (ProjectileData.ProjectileShootClip != null)
            {
                unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(ProjectileData.ProjectileShootClip);
            }

            RB2D.isKinematic = false;
            CC2D.enabled = true;

            if (unit != null)
            {
                RB2D.velocity = new Vector2(ProjectileData.Rot.x * unit.Core.GetCoreComponent<Movement>().fancingDirection, ProjectileData.Rot.y).normalized * ProjectileData.Speed;
            }
            else
            {
                RB2D.velocity = new Vector2(ProjectileData.Rot.x, ProjectileData.Rot.y).normalized * ProjectileData.Speed;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (GameManager.Inst == null)
                return;
            if (GameManager.Inst.PlayTime >= m_startTime + ProjectileData.DurationTime)
            {
                Hit();
            }
        }

        private void OnEnable()
        {
            parent = this.GetComponentInParent<ProjectilePooling>();
        }
        private void OnDisable()
        {
            
        }

        public void Hit(bool _isSingleShoot = true)
        {
            //Impact
            var impact = Instantiate(ProjectileData.ImpactPrefab);
            impact.transform.position = this.transform.position;

            foreach (var _renderer in impact.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                _renderer.sortingLayerName = "Effect";
            }
            var main = impact.GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Destroy;

            if (_isSingleShoot)
            {
                this.gameObject.SetActive(false);
                RB2D.velocity = Vector2.zero;
                RB2D.isKinematic = true;
                if (parent != null)
                {
                    parent.ReturnObject(this.gameObject);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D coll)
        {
            //공격한 주체가 없을 때 무시
            if (unit == null)
            {
                return;
            }

            //같은 Tag는 무시
            if (coll.tag == this.tag)
            {
                return;
            }

            //트랩에는 데미지X
            if (coll.gameObject.tag == "Trap")
                return;

            //피격 대상이 Ground면 이펙트
            if (coll.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Hit(ProjectileData.isSingleShoot);
                return;
            }

            //Damagable이 아니면 무시
            if (coll.gameObject.layer != LayerMask.NameToLayer("Damagable"))
            {
                return;
            }

            //피격 대상 사망 시 무시
            if (coll.gameObject.GetComponentInParent<Unit>().Core.GetCoreComponent<Death>().isDead)
            {
                return;
            }

            //피격대상이 Enemy일 때 Enemy의 공격대상 설정
            if (coll.gameObject.GetComponentInParent<Enemy>() != null)
            {
                coll.gameObject.GetComponentInParent<Enemy>().SetTarget(unit);
            }

            //피격 대상에게 Damage
            //Damagable.cs를 가지고있는 collision이랑 부딪쳤을 때
            if (coll.TryGetComponent(out IDamageable damageable))
            {
                //히트 시 효과
                unit.Inventory.ItemEffectExecute(unit, coll.GetComponentInParent<Unit>());

                Debug.Log($"Projectile Touch {coll.name}");
                //Impact EffectPrefab
                #region EffectPrefab
                if (ProjectileData.ImpactPrefab != null)
                {
                    Hit(ProjectileData.isSingleShoot);
                }
                #endregion

                //Impact AudioClip
                #region AudioClip
                if (ProjectileData.ImpactClip != null)
                {
                    unit.Core.GetCoreComponent<SoundEffect>().AudioSpawn(ProjectileData.ImpactClip);
                }
                #endregion


                //ShakeCam
                #region ShakeCam
                if (ProjectileData.isShakeCam)
                {
                    Camera.main.GetComponent<CameraShake>()?.Shake(
                        ProjectileData.camDatas.RepeatRate,
                        ProjectileData.camDatas.Range,
                        ProjectileData.camDatas.Duration
                        );
                }
                #endregion

                #region Damage
                //플레이어가 피격 당할 때는 EnemyType이 없기때문에 생략
                if (coll.gameObject.tag == "Player")
                {
                    damageable.Damage
                    (
                        unit.Core.GetCoreComponent<UnitStats>().StatsData + ProjectileData.Stats,
                        coll.GetComponentInParent<Unit>().UnitData.statsStats,
                        unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower
                    );
                }
                else
                {
                    //Damage
                    switch (coll.GetComponentInParent<Enemy>().enemyData.enemy_size)
                    {
                        case ENEMY_Size.Small:
                            damageable.Damage
                        (
                            unit.Core.GetCoreComponent<UnitStats>().StatsData + ProjectileData.Stats,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower) * (1.0f + GlobalValue.Enemy_Size_WeakPer)
                        );
                            Debug.Log("Projectile Enemy Type Small, Normal Dam = " +
                                unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower) * (1.0f - GlobalValue.Enemy_Size_WeakPer));
                            break;
                        case ENEMY_Size.Medium:
                            damageable.Damage
                        (
                            unit.Core.GetCoreComponent<UnitStats>().StatsData + ProjectileData.Stats,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower));
                            Debug.Log("Projectile Enemy Type Medium, Normal Dam = " +
                                unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower);
                            break;
                        case ENEMY_Size.Big:
                            damageable.Damage
                        (
                            unit.Core.GetCoreComponent<UnitStats>().StatsData + ProjectileData.Stats,
                            coll.GetComponentInParent<Unit>().UnitData.statsStats,
                            (unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                        );

                            Debug.Log("Projectile Enemy Type Big, Normal Dam = " +
                                unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower
                                + " Enemy_Size_WeakPer Additional Dam = " +
                                (unit.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower + ProjectileData.Stats.DefaultPower) * (1.0f - GlobalValue.Enemy_Size_WeakPer)
                                );
                            break;
                    }
                }
                #endregion
            }

            //피격 대상에게 KnockBack
            //KnockBack
            #region KnockBack
            if (coll.TryGetComponent(out IKnockBackable knockbackables))
            {
                knockbackables.KnockBack(ProjectileData.KnockbackAngle, ProjectileData.KnockbackAngle.magnitude, unit ? unit.Core.GetCoreComponent<Movement>().FancingDirection : 1);
            }
            #endregion
        }
    }
}