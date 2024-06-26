using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerFire : FireSystem
{
    [SerializeField]private PlayerFollow _playerFollow;
    
    //SoundOfFire
    [SerializeField] AudioClip Shot01;
    [SerializeField] AudioClip reload;
    
    //Choosing Weapon
    public int chooseWeapon;
    //InputSystem
    [SerializeField] PlayerInputSystem InputSystem;

    void Start()
    {
        ApplyWeapon(weapons[chooseWeapon]);
        InputSystem = GetComponent<PlayerInputSystem>();
    }

    public void SwitchFromPickUP(int weapon)
    {
        chooseWeapon = weapon;
    }

    //The fire function
    void PlayerFireController()
    {
        // if player Ok to shot and pressing the fire button then...
        if (InputSystem.FireTrigger && OkToShot)
        {
            FireActionDir();
            audioSource.PlayOneShot(Shot01, 0.3f);
        } 
        // Automatic reload when trying to shot with 0 ammo || In the future i'll make also to function to press a key to reload
        else if (_currentAmmo <= 0 && chooseWeapon != 0)
        {
            Reload();
        }
    }

    void Update()
    {
        PlayerFireController(); // fire controller
        IsItOkToShot(); // cool down and ammo capacity check
        SwitchWeapon(chooseWeapon); // Switch weapon function
    }
}
