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
        public Unit unit;
        public ProjectileData ProjectileData;
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
                this.tag = unit.tag;
            this.transform.position = ProjectileData.Pos;
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
        }

        public void SetUp(Unit _unit, ProjectileData m_ProjectileData)
        {
            unit = _unit;

            ProjectileData = m_ProjectileData;
            SetUp();
        }
        public void SetUp(ProjectileData m_ProjectileData)
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

            RB2D.isKinematic = false;
            CC2D.enabled = true;

            RB2D.velocity = new Vector2(ProjectileData.Rot.x * FancingDirection, ProjectileData.Rot.y).normalized * ProjectileData.Speed;
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

        public void Hit(bool _isSingleShoot = true)
        {
            if (_isSingleShoot)
            {
                this.gameObject.SetActive(false);
                RB2D.velocity = Vector2.zero;
                RB2D.isKinematic = true;
            }

            //Impact
            var impact = Instantiate(ProjectileData.ImpactPrefab);
            impact.transform.position = this.transform.position;

            foreach (var _renderer in impact.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                _renderer.sortingLayerName = "Effect";
            }
            var main = impact.GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Destroy;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (unit == null)
            {
                return;
            }

            if (collision.tag == this.tag)
            {
                return;
            }

            if (collision.gameObject.tag == "Trap")
                return;

            //Damagable.cs를 가지고있는 collision이랑 부딪쳤을 때
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log($"Projectile Touch {collision.name}");

                Hit(ProjectileData.isSingleShoot);
            }
        }
    }
}
