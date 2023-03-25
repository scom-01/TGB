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

    private int currentActionSpriteIndex;
    private int currentSpriteCommandIndex;
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentActionSpriteIndex = 0;
    }

    private void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        if(currentGroundedActionData !=null && currentAirActionData !=null)
        {
            if (weapon.BaseGameObject.GetComponent<Animator>().GetBool("inAir"))
            {
                CheckAttackAction(currentAirActionData);
            }
            else
            {
                CheckAttackAction(currentGroundedActionData);
            }
        }
        else if(currentGroundedActionData == null)
        {
            CheckAttackAction(currentAirActionData);
        }
        else if(currentAirActionData == null)
        {
            CheckAttackAction(currentGroundedActionData);
        }
        currentActionSpriteIndex++;
    }
    private void CheckAttackAction(ActionSprites actionSprites)
    {
        if (actionSprites == null)
            return;

        Sprite[] currentAttackSprite = new Sprite[0];

        var currSprites = actionSprites.WeaponSprites;
        if (currSprites.Length <= 0)
            return;

        for (int i = 0; i < currSprites.Length; i++)
        {
            if (currSprites[i].Command == weapon.Command)
            {
                currentSpriteCommandIndex = i;
                break;
            }
        }

        if (weapon.BaseGameObject.GetComponent<Animator>().GetBool("inAir"))
        {
            if (currentGroundedActionData.WeaponSprites.Length > 0)
            {
                currentAttackSprite = currSprites[currentSpriteCommandIndex].sprites;
            }
        }
        else
        {
            if (currentGroundedActionData.WeaponSprites.Length > 0)
                currentAttackSprite = currSprites[currentSpriteCommandIndex].sprites;
        }

        if (currentAttackSprite.Length < 0)
            return;

        if (currentActionSpriteIndex >= currentAttackSprite.Length)
        {
            Debug.Log($"{weapon.name} weapon sprite length mismatch");
            return;
        }

        weaponSpriteRenderer.sprite = currentAttackSprite[currentActionSpriteIndex];
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
