using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSprite : WeaponComponent
{
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;

    [SerializeField]
    private WeaponSprites[] WeaponSprites;

    protected override void Awake()
    {
        base.Awake();
        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
    }

}

[Serializable]
public class WeaponSprites
{ 
    [field: SerializeField]
    public Sprite[] sprites { get; private set; }
}

