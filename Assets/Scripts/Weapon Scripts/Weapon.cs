using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float weaponRythem = 0.6f;
    [SerializeField] int magazineFullCapacity = 6;
    [SerializeField] int currentAmmo = 6;
    [SerializeField] float damage = 1;
    [SerializeField] float timeToReload = 5;
    [SerializeField] float bulletSpeed = 25;
    [SerializeField] int bulletAmount = 1;

    public float GetWeaponRythem() { return weaponRythem; }
    public int GetMagazineFullCapacity() { return magazineFullCapacity; }
    public int GetCurrentAmmo() { return currentAmmo; }
    public void SetCurrentAmmo(int newAmmo) { currentAmmo = newAmmo; }
    public float GetDamage() { return damage; }
    public float GetTimeToReload() { return timeToReload; }
    public float GetBulletSpeed() { return bulletSpeed; }
    public int GetBulletAmount() { return bulletAmount; }
    public void ExtraBulletAmount(int amount) { bulletAmount += amount; }
    public void SpeedUpBulletAmount(float amount) { weaponRythem -= amount; }
    //constructor for new weapon using new peramaters
    public Weapon(float _weaponRythem, int _magazineFullCapacity, int _currentAmmo, float _damage,
        float _timeToReload, float _bulletSpeed, int _bulletAmount)
    {
        weaponRythem = _weaponRythem;
        magazineFullCapacity = -magazineFullCapacity;
        currentAmmo = _currentAmmo;
        damage = _damage;
        timeToReload = _timeToReload;
        bulletSpeed = _bulletSpeed;
        bulletAmount = _bulletAmount;
    }

    //constructor for new weapon using an existing weapon
    public Weapon(Weapon weapon)
    {
        weaponRythem = weapon.weaponRythem;
        magazineFullCapacity = weapon.magazineFullCapacity;
        currentAmmo = weapon.currentAmmo;
        damage = weapon.damage;
        timeToReload = weapon.timeToReload;
        bulletSpeed = weapon.bulletSpeed;
        bulletAmount = weapon.bulletAmount;
    }

}
