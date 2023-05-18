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

        GameManager.Inst.ReforgingUI.EnabledChildrensCanvas(true);
        GameManager.Inst.ReforgingUI.equipWeapon.SetWeaponCommandData(GameManager.Inst.StageManager.player.Inventory.weaponData.weaponCommandDataSO);
        GameManager.Inst.StageManager.player.Inventory.weaponData = GameManager.Inst.StageManager.player.Inventory.weaponData;
        GameManager.Inst.ReforgingUI.equipWeapon.Btn.onClick?.Invoke();
        //GameManager.Inst.SubUI.InventorySubUI.NullCheckInput();
        //GameManager.Inst.inputHandler.ChangeCurrentActionMap("UI", true);
        GameManager.Inst.ReforgingUI.Canvas.enabled = true;
    }
}
