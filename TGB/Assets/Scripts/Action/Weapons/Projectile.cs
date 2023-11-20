using TGB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;
using static UnityEditor.PlayerSettings;

namespace TGB
{
    public class Projectile : MonoBehaviour
    {
        //공격하는 주체
        [SerializeField] private Unit unit;
        //지정 타겟
        private Unit target;
        [HideInInspector] public ProjectileData ProjectileData;

        public GameObject ProjectileObject;
        public GameObject ImpactObject;
        [Space(10)]
        public AudioClip ProjectileShootClip;
        public AudioClip ImpactClip;

        private ProjectilePooling parent;
        private GameObject Spawned_ProjectileObject;

        public int FancingDirection
        {
            get
            {
                if (unit == null)
                    return 1;
                return unit.Core.CoreMovement.FancingDirection;
            }
        }
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
                this.transform.position = unit.Core.CoreCollisionSenses.UnitCenterPos + Vector3.right * ProjectileData.Pos.x * FancingDirection + Vector3.up * ProjectileData.Pos.y;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Projectile");
            this.transform.rotation = Quaternion.Euler(ProjectileData.Rot);


            CC2D.radius = ProjectileData.Radius;
            CC2D.offset = ProjectileData.Offset;
            CC2D.enabled = false;
            RB2D.gravityScale = ProjectileData.GravityScale;

            //set ProjectilPrefab
            Spawned_ProjectileObject = Instantiate(ProjectileObject, this.transform);

            if (ProjectileData.EffectScale == Vector3.zero)
                Spawned_ProjectileObject.gameObject.transform.localScale = Vector3.one;
            else
                Spawned_ProjectileObject.gameObject.transform.localScale = ProjectileData.EffectScale;

            var main = Spawned_ProjectileObject.GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Disable;
            foreach (var _renderer in Spawned_ProjectileObject.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                _renderer.sortingLayerName = "Effect";
            }
            this.gameObject.SetActive(false);
        }

        public void SetUp(Unit _unit, ProjectileData m_ProjectileData)
        {
            unit = _unit;
            target = unit.TargetUnit;

            ProjectileData = m_ProjectileData;

            //Transform, Collider2D
            if (unit != null)
            {
                this.tag = unit.tag;
                this.transform.position = unit.Core.CoreCollisionSenses.UnitCenterPos + Vector3.right * ProjectileData.Pos.x * FancingDirection + Vector3.up * ProjectileData.Pos.y;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Projectile");
            this.transform.rotation = Quaternion.Euler(ProjectileData.Rot);

            CC2D.radius = ProjectileData.Radius;
            CC2D.offset = ProjectileData.Offset;
            CC2D.enabled = false;
            RB2D.gravityScale = ProjectileData.GravityScale;

            if (Spawned_ProjectileObject != null)
            {
                if (ProjectileData.EffectScale == Vector3.zero)
                    Spawned_ProjectileObject.gameObject.transform.localScale = Vector3.one;
                else
                    Spawned_ProjectileObject.gameObject.transform.localScale = ProjectileData.EffectScale;
            }
        }

        public void Init(ProjectileData m_ProjectileData)
        {
            ProjectileData = m_ProjectileData;
            SetUp();
        }

        //발사체 투척
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

            if (ProjectileShootClip != null)
            {
                unit.Core.CoreSoundEffect.AudioSpawn(ProjectileShootClip);
            }

            RB2D.isKinematic = false;
            CC2D.enabled = true;

            if (unit != null)
            {
                //Default가 0을 전제
                if (FancingDirection < 0)
                {
                    RB2D.rotation = 180f;
                }
                if (ProjectileData.homingType == HomingType.isToTarget_Direct && unit.TargetUnit != null                    
                    )                 
                {
                    if (
                        ((unit.TargetUnit.Core.CoreCollisionSenses.GroundCenterPos - this.transform.position).x < -0.001f && FancingDirection < 0) ||
                        ((unit.TargetUnit.Core.CoreCollisionSenses.GroundCenterPos - this.transform.position).x > 0.001f && FancingDirection > 0)
                        )
                    {
                        Vector3 toTargetNormal = (unit.TargetUnit.Core.CoreCollisionSenses.GroundCenterPos - unit.Core.CoreCollisionSenses.GroundCenterPos);
                        this.transform.rotation = Quaternion.FromToRotation(ProjectileData.Rot, toTargetNormal);
                        RB2D.velocity = new Vector2(toTargetNormal.x, toTargetNormal.y).normalized * ProjectileData.Speed;
                    }       
                    else
                    {
                        RB2D.velocity = new Vector2(ProjectileData.Rot.x, ProjectileData.Rot.y).normalized * ProjectileData.Speed;
                    }
                }
                else if(
                    ProjectileData.homingType == HomingType.isToTarget && unit.TargetUnit != null
                    )
                {
                    Vector3 toTargetNormal = (unit.TargetUnit.Core.CoreCollisionSenses.GroundCenterPos - this.transform.position);
                    this.transform.rotation = Quaternion.FromToRotation(ProjectileData.Rot, toTargetNormal);
                    RB2D.velocity = new Vector2(toTargetNormal.x, toTargetNormal.y).normalized * ProjectileData.Speed;
                }
                else
                    RB2D.velocity = new Vector2(ProjectileData.Rot.x * FancingDirection, ProjectileData.Rot.y).normalized * ProjectileData.Speed;
            }
            else
            {
                RB2D.velocity = new Vector2(ProjectileData.Rot.x, ProjectileData.Rot.y).normalized * ProjectileData.Speed;
            }
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (GameManager.Inst == null)
                return;

            if (target != null && ProjectileData.homingType == HomingType.isHoming)
            {
                Vector2 dir = transform.right;
                Vector3 targetDir = (target.gameObject.transform.position - transform.position).normalized;

                // 바라보는 방향과 타겟 방향 외적
                Vector3 crossVec = Vector3.Cross(dir, targetDir);
                // 상향 벡터와 외적으로 생성한 벡터 내적
                float inner = Vector3.Dot(Vector3.forward, crossVec);
                // 내적이 0보다 크면 오른쪽 0보다 작으면 왼쪽으로 회전
                float addAngle = inner > 0 ? 500 * Time.fixedDeltaTime : -500 * Time.fixedDeltaTime;
                float saveAngle = addAngle + transform.rotation.eulerAngles.z;
                //transform.rotation = Quaternion.Euler(0, 0, saveAngle);
                RB2D.rotation += addAngle;
                float moveDirAngle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
                var moveDir = new Vector2(Mathf.Cos(moveDirAngle), Mathf.Sin(moveDirAngle));

                Vector2 movePosition = RB2D.position + (moveDir * ProjectileData.Speed) * Time.fixedDeltaTime;
                RB2D.MovePosition(movePosition);
            }

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

        private void Hit(bool _isSingleShoot = true)
        {
            //Impact            
            if (unit != null)
            {
                var impact = unit.Core.CoreEffectManager.StartEffectsPos(ImpactObject, this.transform.position, ProjectileData.EffectScale);
                foreach (var _renderer in impact.GetComponentsInChildren<ParticleSystemRenderer>())
                {
                    _renderer.sortingLayerName = "Effect";
                }
                var main = impact.GetComponent<ParticleSystem>().main;
                main.stopAction = ParticleSystemStopAction.Disable;
            }

            //Impact AudioClip
            #region AudioClip
            if (unit != null && ImpactClip != null)
            {
                unit.Core.CoreSoundEffect.AudioSpawn(ImpactClip);
            }
            #endregion

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

        private void OnTriggerEnter2D(Collider2D coll)
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
            if (ProjectileData.isCollisionGround)
            {
                if (coll.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    Hit(ProjectileData.isSingleShoot);
                    return;
                }
            }

            //Damagable이 아니면 무시
            if (coll.gameObject.layer != LayerMask.NameToLayer("Damagable"))
            {
                return;
            }

            //피격 대상 사망 시 무시
            if (coll.gameObject.GetComponentInParent<Unit>().Core.CoreDeath.isDead)
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
                if (ProjectileData.isOnHit)
                {
                    //히트 시 효과
                    unit.Inventory.ItemOnHitExecute(unit, coll.GetComponentInParent<Unit>());
                }

                Debug.Log($"Projectile Touch {coll.name}");
                //Impact EffectPrefab
                #region EffectPrefab
                if (ImpactObject != null)
                {
                    Hit(ProjectileData.isSingleShoot);
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

                if (ProjectileData.isFixed)
                {
                    damageable.FixedDamage((int)ProjectileData.AdditionalDmg, true);
                }
                else
                {
                    damageable.TypeCalDamage(unit, coll.GetComponentInParent<Unit>(), unit.Core.CoreUnitStats.DefaultPower + ProjectileData.AdditionalDmg);
                }
                #endregion
            }

            //피격 대상에게 KnockBack
            //KnockBack
            #region KnockBack
            if (ProjectileData.KnockbackAngle.magnitude == 0)
                return;
            if (coll.TryGetComponent(out IKnockBackable knockbackables))
            {
                knockbackables.KnockBack(ProjectileData.KnockbackAngle, ProjectileData.KnockbackAngle.magnitude, FancingDirection);
            }
            #endregion
        }
    }
}