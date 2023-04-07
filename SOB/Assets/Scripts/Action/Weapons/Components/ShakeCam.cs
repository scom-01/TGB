using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class ShakeCam : WeaponComponent<ShakeCamData, ActionShakeCam>
    {
        private int currentShakeCaIdx;       

        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentShakeCaIdx = 0;
        }
        public void HandleShakeCam()
        {
            if (Camera.main.GetComponent<CameraShake>() == null)
            {
                Debug.LogWarning("The main camera does not have the CameraShake component.");
                return;
            }
            Debug.LogWarning($"{weapon.weaponData.name} {currentActionData} ??");
            CheckCamAction(currentActionData);
            currentShakeCaIdx++;
        }

        public bool CheckCamAction(ActionShakeCam actionShakeCam)
        {
            if (actionShakeCam == null)
            {
                Debug.LogWarning("ActionShakeCam is null");
                return false;
            }
            var currCamAction = actionShakeCam.ShakeCamData;
            if (currCamAction.Length <= 0)
                return false;


            if (currentShakeCaIdx >= currCamAction.Length)
            {
                return false;
            }
            Debug.Log("Shake Cam");
            Camera.main.GetComponent<CameraShake>().Shake(
                currCamAction[currentShakeCaIdx].ShakgeCamRepeatRate,
                currCamAction[currentShakeCaIdx].ShakeCamRange,
                currCamAction[currentShakeCaIdx].ShakeCamDuration
                );
            return true;
        }
        protected override void Start()
        {
            base.Start();

            eventHandler.OnShakeCam += HandleShakeCam;
        }

        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnShakeCam -= HandleShakeCam;
        }
    }
}
