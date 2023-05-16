using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipWeapon : WeaponMiniPanel
{
    protected override void OnEnable()
    {
        if (GameManager.Inst != null) ;
        {
            if (GameManager.Inst.StageManager != null)
            {
                weaponCommandDataSO = GameManager.Inst.StageManager.player.Inventory.weaponDatas[0].weaponCommandDataSO;
            }
        }

        base.OnEnable();
    }
    public void SetReforgingWeapon(ReforgingWeapon data)
    {
        if(this.weaponCommandDataSO == null)
        {
            Debug.LogWarning($"{this.name} WeaponCommandDataSO is Null");
            return;
        }
        data.parentWeaponCommandDataSO = this.weaponCommandDataSO;
        if (this.weaponCommandDataSO.UpgradeWeaponCommandDataSO[data.ReforgingWeaponIdx] != null)
            data.SetWeaponCommandData(this.weaponCommandDataSO.UpgradeWeaponCommandDataSO[data.ReforgingWeaponIdx]);
    }
}