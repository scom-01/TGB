using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReforgingAnvil : InteractiveObject
{
    public override void Interactive()
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
            return;

        if (GameManager.Inst.ReforgingUI.equipWeapon == null)
            return;

        GameManager.Inst.ReforgingUI.equipWeapon.SetWeaponCommandData(GameManager.Inst.StageManager.player.Inventory.weaponData.weaponCommandDataSO);
        GameManager.Inst.ReforgingUI.equipWeapon.Btn.onClick?.Invoke();

        GameManager.Inst.ReforgingUI.Canvas.enabled = true;
    }
}
