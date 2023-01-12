using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Core Core { get; private set; }
    public UnitFSM FSM { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerStats PS { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public BoxCollider2D BC2D { get; private set; }

    public SpriteRenderer SR { get; private set; }

    public Inventory Inventory { get; private set; }

    public UnitData UnitData;
    
    #region Unity Callback Func
    protected virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
        FSM = new UnitFSM();

        if (UnitData == null)
        {
            Debug.Log($"{this.name} UnitData is null");
        }
    }
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        if (Anim == null) Anim = this.GameObject().AddComponent<Animator>();
        
        RB = GetComponent<Rigidbody2D>();
        if (RB == null) RB = this.GameObject().AddComponent<Rigidbody2D>();

        BC2D = GetComponent<BoxCollider2D>();
        if (BC2D == null) BC2D = this.GameObject().AddComponent<BoxCollider2D>();

        SR = GetComponent<SpriteRenderer>();
        if (SR == null) SR = this.GameObject().AddComponent<SpriteRenderer>();

        Inventory = GetComponent<Inventory>();
        if (Inventory == null) Inventory = this.GameObject().AddComponent<Inventory>();

        PS = GetComponent<PlayerStats>();
        if (PS == null) PS = this.GameObject().AddComponent<PlayerStats>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Core != null)
        {

            Core.LogicUpdate();
        }
        else
        {
            Debug.Log("Core is null");
        }
        FSM.CurrentState.LogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        FSM.CurrentState.PhysicsUpdate();
    }
    #endregion Unity Callback Func
}