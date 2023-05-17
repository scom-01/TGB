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

    public void ClearReforgigngMaterial(ReforgingMaterial reforgingMaterial)
    {
        reforgingMaterial.ClearRendering();
    }

    public override void SetWeaponCommandData(WeaponCommandDataSO dataSO)
    {
        base.SetWeaponCommandData(dataSO);
        Btn.onClick?.Invoke();
    }
    public void SetReforgingWeapon(ReforgingWeapon data)
    {
        if(this.weaponCommandDataSO == null)
        {
            Debug.LogWarning($"{this.name} WeaponCommandDataSO is Null");
            return;
        }
        data.parentWeaponCommandDataSO = this.weaponCommandDataSO;
        if (0 < this.weaponCommandDataSO.UpgradeWeaponCommandDataSO.Count && data.ReforgingWeaponIdx < this.weaponCommandDataSO.UpgradeWeaponCommandDataSO.Count)
        {
            data.SetWeaponCommandData(this.weaponCommandDataSO.UpgradeWeaponCommandDataSO[data.ReforgingWeaponIdx]);
        }
        else
        {
            Debug.LogWarning($"{this.weaponCommandDataSO.UpgradeWeaponCommandDataSO}[{data.ReforgingWeaponIdx}] WeaponCommandDataSO is Null");
        }
    }
}
