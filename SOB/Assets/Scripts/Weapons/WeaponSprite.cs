using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SOB.Weapons.Components;
public class WeaponSprite : WeaponComponent
{
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;
    private GameObject baseObject;

    private WeaponSpriteData GroundedWeaponSprites;
    private WeaponSpriteData InAirWeaponSprites;

    private int currentWeaponSpriteIndex;
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }

    private void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if(!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }
        Sprite[] currentAttackSprite = new Sprite[0];

        if (baseObject.GetComponent<Animator>().GetBool("inAir"))
        {
            if(InAirWeaponSprites.AttackData.Length> 0)
                currentAttackSprite = InAirWeaponSprites.AttackData[weapon.CurrentAttackCounter].Sprites;
        }
        else
        {
            if (GroundedWeaponSprites.AttackData.Length > 0)
                currentAttackSprite = GroundedWeaponSprites.AttackData[weapon.CurrentAttackCounter].Sprites;
        }

        if (currentAttackSprite.Length < 0)
            return;

        if(currentWeaponSpriteIndex >= currentAttackSprite.Length)
        {
            Debug.Log($"{weapon.name} weapon sprite length mismatch");
            return;
        }

        weaponSpriteRenderer.sprite = currentAttackSprite[currentWeaponSpriteIndex];        
        currentWeaponSpriteIndex++;
    }

    protected override void Awake()
    {
        base.Awake();
        baseObject = transform.Find("Base").gameObject;
        baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = transform.Find("Weapon").GetComponent<SpriteRenderer>();
        //baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        //weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

        weapon.OnEnter += HandleEnter;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
        weapon.OnEnter -= HandleEnter;
    }
}
