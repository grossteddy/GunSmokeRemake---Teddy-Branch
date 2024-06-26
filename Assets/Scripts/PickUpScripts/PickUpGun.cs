using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PickUpGun : PickUpable
{
    [SerializeField] int weaponType;
    //[SerializeField] AvatarMask avatarMask;
    bool canPickNow;
    float timer = 2;
    protected override void OnPickUp()
    {
        if (canPickNow)
        {
            var lastGunScript = player.GetComponentInChildren<PickUpGun>();
            if (lastGunScript != null)
            {

                var lastGun = lastGunScript.gameObject;
                lastGun.transform.GetChild(0).gameObject.SetActive(true);
                lastGun.transform.SetParent(transform.parent);
            }
           /* else
            {
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Body, false);
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightArm, false);
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFingers, false);
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightHandIK, false);
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftArm, false);
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFingers, false);
                avatarMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftHandIK, false);
            }*/
            player.GetComponent<PlayerFire>().chooseWeapon = weaponType;
            transform.SetParent(player.transform);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            timer = 0;
            
        }
    }
    private void Update()
    {
        if (timer < 2)
        {
            canPickNow = false;
            timer += Time.deltaTime;
        }
        else
        {
            canPickNow = true;
        }
    }
}
