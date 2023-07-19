using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace SOB
{
    public class Projectile : MonoBehaviour
    {
        public Unit unit;
        public ProejectileData ProjectileData;
        private float m_startTime;
        public Rigidbody2D RB2D
        {
            get
            {
                if (rb2d == null)
                {
                    rb2d = this.GetComponent<Rigidbody2D>();
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
                    cc2d.isTrigger = true;
                }
                return cc2d;
            }
        }

        private CircleCollider2D cc2d;
        public Projectile(Unit _unit ,Vector3 pos = new Vector3(), Vector3 rot = new Vector3(), float radius = 0, bool _isSingleShoot = true, float gravity = 0, GameObject _projectilePrefab = null, GameObject _impactPrefab = null)
        {
            unit = _unit;
            if(unit != null)
            {
                this.tag = unit.tag;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Projectile");
            ProjectileData.Pos = pos;
            ProjectileData.Rot = rot;
            ProjectileData.Radius = radius;
            ProjectileData.isSingleShoot = _isSingleShoot;
            ProjectileData.GravityScale = gravity;
            ProjectileData.ProjectilePrefab = _projectilePrefab;
            ProjectileData.ImpactPrefab = _impactPrefab;
            SetUp();
        }

        
        [ContextMenu("SetUp")]
        private void SetUp()
        {
            //Transform, Collider2D
            this.transform.position = ProjectileData.Pos;
            this.transform.rotation = Quaternion.Euler(ProjectileData.Rot);
            CC2D.radius = ProjectileData.Radius;
            RB2D.gravityScale = ProjectileData.GravityScale;
            
            //set ProjectilPrefab
            var _prefab = Instantiate(ProjectileData.ProjectilePrefab, this.transform);
            var main = _prefab.GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Disable;
            foreach (var _renderer in _prefab.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                _renderer.sortingLayerName = "Effect";
            }

            //set startTime
            if (GameManager.Inst != null)
            {
                m_startTime = GameManager.Inst.PlayTime;
            }
            else
            {
                m_startTime = Time.time;
            }
        }

        private void SetUp(Vector3 pos,Vector3 rot, float radius, float gravity, GameObject _particlePrefab)
        {
            ProjectileData.Pos = pos;
            this.transform.position = ProjectileData.Pos;
            ProjectileData.Rot = rot;
            this.transform.rotation = Quaternion.Euler(ProjectileData.Rot);
            ProjectileData.Radius = radius;
            CC2D.radius = ProjectileData.Radius;
            ProjectileData.GravityScale = gravity;
            RB2D.gravityScale = ProjectileData.GravityScale;
            ProjectileData.ProjectilePrefab = _particlePrefab;
            var prefab = Instantiate(ProjectileData.ProjectilePrefab, this.transform);
            if (prefab.GetComponent<ParticleSystem>() != null) 
            {
                ParticleSystem.MainModule main = prefab.GetComponent<ParticleSystem>().main;
                main.loop = true;                
            }
        }
        
        public void Shoot()
        {
            RB2D.isKinematic = false;
            RB2D.velocity = ProjectileData.Rot.normalized * ProjectileData.Speed;

        }
        // Update is called once per frame
        void Update()
        {
            if (GameManager.Inst == null)
                return;
            if(GameManager.Inst.PlayTime >= m_startTime + ProjectileData.DurationTime)
            {
                Hit();
            }
        }

        public void Hit(bool _isSingleShoot = true)
        {
            if(_isSingleShoot)
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

            if(collision.tag == this.tag)
            {
                return;
            }

            if (collision.gameObject.tag == "Trap")
                return;

            if (collision.gameObject.layer != 1 << unit.UnitData.WhatIsEnemyUnit)
            {
                return;
            }

            //Damagable.cs를 가지고있는 collision이랑 부딪쳤을 때
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log($"Projectile Touch {collision.name}");

                Hit(ProjectileData.isSingleShoot);
            }
        }
    }
}
