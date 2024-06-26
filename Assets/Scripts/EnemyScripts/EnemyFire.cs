using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyFire : FireSystem
{
    //Choosing Weapon
    [SerializeField] ChooseWeapon chooseWeapon = new ChooseWeapon();
    
    void Start()
    {
        ApplyWeapon(weapons[(int)chooseWeapon]);
        weapons[0] = new Weapon(weapons[0]);
    }

    void Update()
    {
        IsItOkToShot(); // cool down and ammo capacity check
        SwitchWeapon((int)chooseWeapon); // Switch weapon function
    }

    public void EnemyFireController()
    {
        // if player Ok to shot
        if (OkToShot)
        {
            FireAction();
            audioSource.Play();
        }
        // Automatic reload when trying to shot with 0 ammo
        else if (_currentAmmo <= 0)
        {
            Reload();
        }
    }
}
