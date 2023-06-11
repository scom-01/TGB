using SOB.CoreSystem;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.IK;
using static UnityEngine.Rendering.DebugUI;

public class Unit : MonoBehaviour
{
    #region Component
    public Core Core
    {
        get
        {
            if (core == null)
                core = this.GetComponentInChildren<Core>();
            return core;
        }
        private set
        {
            core = value;
        }
    }
    private Core core;
    public UnitFSM FSM { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public CapsuleCollider2D CC2D
    {
        get
        {
            if (cc2d == null)
            {
                cc2d = this.GetComponent<CapsuleCollider2D>();
                if (cc2d != null)
                {
                    return cc2d;
                }
                cc2d = this.GameObject().AddComponent<CapsuleCollider2D>();
                cc2d.sharedMaterial = UnitData.UnitCC2DMaterial ?? UnitData.UnitCC2DMaterial;
                cc2d.offset = UnitData.standCC2DOffset;
                cc2d.size = UnitData.standCC2DSize;
            }
            return cc2d;
        }
        private set
        {
            cc2d = value;
        }
    }

    private CapsuleCollider2D cc2d;

    public SpriteRenderer SR { get; private set; }

    public Inventory Inventory { get; private set; }

    public Transform RespawnPoint;

    public UnitData UnitData;

    public bool IsAlive = true;
    private DamageFlash[] DamageFlash;

    /// <summary>
    /// 절대 면역값
    /// </summary>
    public bool isImmunity = false;

    /// <summary>
    /// 면역값
    /// </summary>
    public bool isCCimmunity
    {
        get
        {
            if (isImmunity)
            {
                _isCCimmunity = true;
            }
            return _isCCimmunity;
        }
        set
        {
            _isCCimmunity = value;
            if (isImmunity)
            {
                _isCCimmunity = true;
            }
        }
    }

    private bool _isCCimmunity;

    /// <summary>
    /// 타겟 유닛
    /// </summary>
    [HideInInspector]

    public Unit TargetUnit { get; private set; }

    #endregion

    #region Unity Callback Func
    protected virtual void Awake()
    {
        InitSetting();
        DamageFlash = GetComponentsInChildren<DamageFlash>();
    }

    private void InitSetting()
    {
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

        RB.gravityScale = UnitData.UnitGravity;

        CC2D = GetComponent<CapsuleCollider2D>();
        if (CC2D == null) CC2D = this.GameObject().AddComponent<CapsuleCollider2D>();

        SR = GetComponent<SpriteRenderer>();
        if (SR == null) SR = this.GameObject().AddComponent<SpriteRenderer>();

        Inventory = GetComponent<Inventory>();
        if (Inventory == null) Inventory = this.GameObject().AddComponent<Inventory>();

    }

    protected virtual void Start()
    {
        RespawnPoint = GameManager.Inst.StageManager.respawnPoint.transform;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckLife(this);

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
                Debug.Log(name + " isHit false");
                Core.GetCoreComponent<UnitStats>().invincibleTime = 0f;
            }
        }

        if (Core.GetCoreComponent<UnitStats>().TouchinvincibleTime > 0.0f)
        {
            Core.GetCoreComponent<UnitStats>().TouchinvincibleTime -= Time.deltaTime;

            if (Core.GetCoreComponent<UnitStats>().TouchinvincibleTime <= 0.0f)
            {
                Core.GetCoreComponent<DamageReceiver>().isTouch = false;
                Core.GetCoreComponent<UnitStats>().TouchinvincibleTime = 0f;
            }
        }

        FSM.CurrentState.LogicUpdate();
    }

    public void SetTarget(Unit unit)
    {
        if (unit == null)
            return;

        TargetUnit = unit;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "DeadArea")
            return;

        Core?.GetCoreComponent<Movement>().SetVelocityZero();

        //지정된 리스폰 위치로 이동
        if (GameManager.Inst?.StageManager?.respawnPoint != null)
            this.gameObject.transform.position = RespawnPoint.position;

        var amount = Core.GetCoreComponent<UnitStats>().DecreaseHealth(E_Power.Normal, DAMAGE_ATT.Fixed, 10);
        if (Core.GetCoreComponent<DamageReceiver>().DefaultEffectPrefab == null)
        {
            Core.GetCoreComponent<DamageReceiver>().RandomEffectInstantiate(0.5f, amount, 50, DAMAGE_ATT.Fixed);
        }
        else
        {
            Core.GetCoreComponent<DamageReceiver>().RandomEffectInstantiate(Core.GetCoreComponent<DamageReceiver>().DefaultEffectPrefab, 0.5f, amount, 50, DAMAGE_ATT.Fixed);
        }

    }

    private void CheckLife(Unit unit)
    {
        unit.IsAlive = unit.gameObject.activeSelf;
    }


    public void AnimationFinishTrigger() => FSM.CurrentState.AnimationFinishTrigger();

    public virtual void HitEffect()
    {
        foreach (var flash in DamageFlash)
        {
            flash.CallFlashWhite();
        }
    }

    public virtual void DieEffect()
    {
    }

    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
    public IEnumerator DisableCollision()
    {
        if (CC2D != null)
        {
            CC2D.isTrigger = true;
        }
        yield return new WaitForSeconds(0.2f);
        if (CC2D != null)
        {
            CC2D.isTrigger = false;
        }
    }

    protected virtual void FixedUpdate()
    {
        FSM.CurrentState.PhysicsUpdate();
    }
    #endregion Unity Callback Func
}
