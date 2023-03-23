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
        Sprite[] currentAttackSprite = new Sprite[0];

        var currSprites = currentActionData.GroundedSprites;
        if (weapon.BaseGameObject.GetComponent<Animator>().GetBool("inAir"))
        {
            currSprites = currentActionData.InAirSprites;
        }


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
            if (currentActionData.InAirSprites.Length > 0)
            {
                currentAttackSprite = currSprites[currentSpriteCommandIndex].sprites;
            }
        }
        else
        {
            if (currentActionData.GroundedSprites.Length > 0)
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
        currentActionSpriteIndex++;
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
