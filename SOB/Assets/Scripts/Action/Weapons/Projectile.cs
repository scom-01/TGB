using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

namespace SOB
{
    public class Projectile : MonoBehaviour
    {
        public Unit unit;
        public Vector3 Pos = Vector3.zero;
        public Vector3 Rot = Vector3.zero;
        public float Radius = 0;
        public float GravityScale = 0;
        public bool isSingleShoot = true;
        public GameObject ParticlePrefab;

        public Projectile(Unit _unit ,Vector3 pos = new Vector3(), Vector3 rot = new Vector3(), float radius = 0, bool _isSingleShoot = true, float gravity = 0, GameObject _particlePrefab = null)
        {
            unit = _unit;
            if(unit != null)
            {
                this.tag = unit.tag;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Projectile");
            Pos = pos;
            Rot = rot;
            Radius = radius;
            isSingleShoot = _isSingleShoot;
            GravityScale = gravity;
            ParticlePrefab = _particlePrefab;
        }

        public Rigidbody2D RB2D
        {
            get
            {
                if(rb2d == null)
                {
                    rb2d = this.GetComponent<Rigidbody2D>();
                    rb2d.gravityScale = GravityScale;
                }
                return rb2d;
            }
        }
        private Rigidbody2D rb2d;

        private CircleCollider2D CC2D
        {
            get
            {
                if(cc2d == null)
                {
                    cc2d = this.GetComponent<CircleCollider2D>();
                    cc2d.isTrigger = true;
                }
                return cc2d;
            }
        }

        private CircleCollider2D cc2d;


        [ContextMenu("SetUp")]
        private void SetUp()
        {
            this.transform.position = Pos;
            this.transform.rotation = Quaternion.Euler(Rot);
            CC2D.radius = Radius;
            RB2D.gravityScale = GravityScale;
            var prefab = Instantiate(ParticlePrefab, this.transform);
            if (prefab.GetComponent<ParticleSystem>() != null)
            {
                ParticleSystem.MainModule main = prefab.GetComponent<ParticleSystem>().main;
                main.loop = true;
            }
        }

        private void SetUp(Vector3 pos,Vector3 rot, float radius, float gravity, GameObject _particlePrefab)
        {
            Pos = pos;
            this.transform.position = Pos;
            Rot = rot;
            this.transform.rotation = Quaternion.Euler(Rot);
            Radius = radius;
            CC2D.radius = Radius;
            GravityScale = gravity;
            RB2D.gravityScale = GravityScale;
            ParticlePrefab = _particlePrefab;
            var prefab = Instantiate(ParticlePrefab, this.transform);
            if (prefab.GetComponent<ParticleSystem>() != null) 
            {
                ParticleSystem.MainModule main = prefab.GetComponent<ParticleSystem>().main;
                main.loop = true;                
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (unit == null)
            {
                return;
            }

            if (collision.gameObject.layer != 1 << unit.UnitData.WhatIsEnemyUnit)
            {
                return;
            }

            Debug.Log($"Projectile Touch {collision.name}");
            if(isSingleShoot)
            {
                Debug.Log($"Destroy {this.name}");
                Destroy(this);
            }
            else
            {
                Debug.Log($"{this.name} Through {collision.name}");
            }
        }
    }
}
