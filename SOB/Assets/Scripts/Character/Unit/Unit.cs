using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using SOB.CoreSystem;
using System.ComponentModel;

public class Unit : MonoBehaviour
{
    #region Component
    public Core Core { get; private set; }
    public UnitFSM FSM { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC2D { get; private set; }

    public SpriteRenderer SR { get; private set; }

    public Inventory Inventory { get; private set; }

    public UnitData UnitData;

    public bool IsAlive = true;
    #endregion

    #region Unity Callback Func
    protected virtual void Awake()
    {
        InitSetting();
    }

    private void InitSetting()
    {
        Core = GetComponentInChildren<Core>();
        FSM = new UnitFSM();

        if (UnitData == null)
        {
            Debug.Log($"{this.name} UnitData is null");
        }

        Anim = GetComponent<Animator>();
        if (Anim == null)
        {
            Anim = this.GameObject().AddComponent<Animator>();
            Anim.runtimeAnimatorController = UnitData.UnitAnimator;
        };

        RB = GetComponent<Rigidbody2D>();
        if (RB == null) RB = this.GameObject().AddComponent<Rigidbody2D>();

        BC2D = GetComponent<BoxCollider2D>();
        if (BC2D == null)
        {
            BC2D = this.GameObject().AddComponent<BoxCollider2D>();
            BC2D.sharedMaterial = UnitData.UnitColliderMaterial;
            BC2D.offset = UnitData.standColliderOffset;
            BC2D.size = UnitData.standColliderSize;
        }

        SR = GetComponent<SpriteRenderer>();
        if (SR == null) SR = this.GameObject().AddComponent<SpriteRenderer>();

        Inventory = GetComponent<Inventory>();
        if (Inventory == null) Inventory = this.GameObject().AddComponent<Inventory>();
    }

    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckLife(this);
        CheckDeadLine();

        if (Core != null)
            Core.LogicUpdate();
        else
            Debug.Log("Core is null");

        if (Core.GetCoreComponent<UnitStats>().invincibleTime > 0.0f)
        {
            Core.GetCoreComponent<UnitStats>().invincibleTime -= Time.deltaTime;

            if (Core.GetCoreComponent<UnitStats>().invincibleTime <= 0.0f)
            {
                Core.GetCoreComponent<DamageReceiver>().isHit = false;
                Core.GetCoreComponent<UnitStats>().invincibleTime = 0f;
            }
        }

        FSM.CurrentState.LogicUpdate();
    }

    private void CheckDeadLine()
    {
        if (this.transform.position.y < GameManager.Inst.DeadLine)
        {
            Core?.GetCoreComponent<Movement>().SetVelocityZero();
            this.gameObject.transform.position = GameManager.Inst.respawnPoint.transform.position;
            var amount = Core.GetCoreComponent<UnitStats>().DecreaseHealth(E_Power.Normal, DAMAGE_ATT.Fixed, 50);
            if (Core.GetCoreComponent<DamageReceiver>().DefaultEffectPrefab == null)
            {
                Core.GetCoreComponent<DamageReceiver>().RandomParticleInstantiate(0.5f, amount, 50, DAMAGE_ATT.Fixed);
            }
            else
            {
                Core.GetCoreComponent<DamageReceiver>().RandomParticleInstantiate(Core.GetCoreComponent<DamageReceiver>().DefaultEffectPrefab, 0.5f, amount, 50, DAMAGE_ATT.Fixed);
            }


            //player.GetComponent<Player>().Core.GetCoreComponent<Death>().Die();
            //respawn = true;
        }
    }

    private void CheckLife(Unit unit)
    {
        unit.IsAlive = unit.gameObject.activeSelf;
    }


    public void AnimationFinishTrigger() => FSM.CurrentState.AnimationFinishTrigger();

    public virtual void HitEffect()
    {
        var sprites = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            var oldcolor = sprites[i].color;
            StartCoroutine(HitEffect(sprites[i], oldcolor, 0.5f));
        }
    }
    public IEnumerator DisableCollision()
    {
        BC2D.isTrigger = true;
        yield return new WaitForSeconds(0.25f);
        BC2D.isTrigger = false;
    }

    IEnumerator HitEffect(SpriteRenderer sr, Color oldcolor, float duration)
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(duration);
        sr.color = oldcolor;
    }

    protected virtual void FixedUpdate()
    {
        FSM.CurrentState.PhysicsUpdate();
    }
    #endregion Unity Callback Func
}
