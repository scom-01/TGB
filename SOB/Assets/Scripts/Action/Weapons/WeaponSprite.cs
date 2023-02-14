using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using SOB.Weapons.Components;
public class WeaponSprite : WeaponComponent<WeaponSpriteData, ActionSprites>
{
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;
    private GameObject baseObject;

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
        
        if (weapon.BaseGameObject.GetComponent<Animator>().GetBool("inAir"))
        {
            if (currentActionData.InAirSprites.Length > 0)
                currentAttackSprite = currentActionData.InAirSprites;
        }
        else
        {
            if (currentActionData.GroundedSprites.Length > 0)
                currentAttackSprite = currentActionData.GroundedSprites;
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

    protected override void Start()
    {
        base.Start();
        //baseObject = transform.Find("Base").gameObject;
        //baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
        //weaponSpriteRenderer = transform.Find("Weapon").GetComponent<SpriteRenderer>();

        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();

        data = weapon.weaponData.GetData<WeaponSpriteData>();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
    }

    protected override void OnDestory()
    {
        base.OnDestory();

        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
    }
}
