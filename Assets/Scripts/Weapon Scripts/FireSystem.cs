using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireSystem : MonoBehaviour
{
    //Availaible Weapons
    [SerializeField] protected List<Weapon> weapons;
    //FireOrigin
    [SerializeField] Transform _fireOrigin;
    [SerializeField] protected AudioClip _reload;
    [SerializeField] protected AudioSource audioSource;
    [Header("Dependency for Player only")]
    [SerializeField] Transform _fireTarget;

    //Selected Weapon
    private int _selectedWeaponNumber = 0;

    //Weapon Values
    float _weaponRythem;
    float _damage;
    int _magazineFullCapacity;
    protected int _currentAmmo;
    float _fireCoolDownTimer;
    protected bool OkToShot;
    float _reloadTimer;
    float _timeToReload;
    float _bulletSpeed;
    int _bulletAmount;

    //Reload variables;
    protected bool _reloading;
    
    //Bullet direction Values
    private Vector3 _fireTargetVector;
    [SerializeField] private Transform _targetFireObject;
    
    //Getting Pool scripts
    [SerializeField] protected PoolSystem_Bullets _bulletsPool;


    /*protected virtual void SettingShooterDirection(string target)
    {
        _fireOrigin = GameObject.Find(target).GetComponent<Transform>();
    }*/

    // Weapon Types
    protected enum ChooseWeapon
    {
        None,//0
        Revolver,//1
        Rifle, //2
        Dynomite, //3
    }

    //Applying weaopn stats function for each kind
    protected void ApplyWeapon(Weapon weapon)
    {
        _weaponRythem = weapon.GetWeaponRythem();
        _magazineFullCapacity = weapon.GetMagazineFullCapacity();
        _damage = weapon.GetDamage();
        _timeToReload = weapon.GetTimeToReload();
        _currentAmmo = weapon.GetCurrentAmmo();
        _bulletSpeed = weapon.GetBulletSpeed();
        _bulletAmount = weapon.GetBulletAmount();
    }

    //Switch weapon will know which to implament depending on it's weapon type
    protected void SwitchWeapon(int weaponType)
    {
         //Before switch is made, current ammo in this script will be trasfered to the current weapon and stored
         weapons[_selectedWeaponNumber].SetCurrentAmmo(_currentAmmo);
         _selectedWeaponNumber = weaponType;
        
  
        //Not sure there is a need for a switch case
        ApplyWeapon(weapons[weaponType]);


    }

    //Reload Function, checking if the time that was set to reload is passed, only then the ammo becomes full
    protected void Reload()
    {
        _reloadTimer += Time.deltaTime;

        if (_reloadTimer >= _timeToReload)
        {
            _reloadTimer = 0;
            _currentAmmo = _magazineFullCapacity;
            if (_reload != null) { audioSource.PlayOneShot(_reload,1); }
        }
    }

    //Cool-Down and check ammo capacity system
    protected void IsItOkToShot()
    {
        //the timer depends on weapon rythem
        if (_fireCoolDownTimer < _weaponRythem)
        {
            _fireCoolDownTimer += Time.deltaTime;
        }
        //Checking if cool down is over AND if the player has ammo, Only then the player is OK to shot
        if (_fireCoolDownTimer >= _weaponRythem && _currentAmmo > 0)
        {
            OkToShot = true;
        }
        else
        {
            OkToShot = false;
        }
    }

    protected void FireAction() // when shot fired this lines takes action >>
    {
        _fireCoolDownTimer = 0; // setting the cooldown back to zero to start counting again
        GameObject pulledObject; // var to hold the Bullet Gameobject 
        FireVelocity fireVelocity; // the script attached to the bullet to control his direction and velocity
        pulledObject = _bulletsPool.PullBullet(); // pulling the bullet from the pool system script
        pulledObject.transform.position = _fireOrigin.position; // Setting start position for the bullet
        fireVelocity = pulledObject.GetComponent<FireVelocity>(); // getting the bullet script
        fireVelocity.Bullet_Direction = _fireOrigin.forward; // Setting bullet direction into player's front direction
        fireVelocity.BulletSpeed = _bulletSpeed; // Setting bullet speed
        fireVelocity.Damage = (int)_damage; // Setting bullet damage
        _currentAmmo -= 1; // counting down 1 bullet less
    }
    protected void FireActionDir() // when shot fired this lines takes action, with an extra direction value >>
    {
        _fireCoolDownTimer = 0; // setting the cooldown back to zero to start counting again
        GameObject pulledObject; // var to hold the Bullet Gameobject 
        FireVelocity fireVelocity; // the script attached to the bullet to control his direction and velocity
        pulledObject = _bulletsPool.PullBullet(); // pulling the bullet from the pool system script
        pulledObject.transform.position = _fireOrigin.position; // Setting start position for the bullet
        fireVelocity = pulledObject.GetComponent<FireVelocity>(); // getting the bullet script
        fireVelocity.Bullet_Direction =  (_fireTarget.position - _fireOrigin.position).normalized; // Setting bullet direction into player's front direction
        fireVelocity.BulletSpeed = _bulletSpeed; // Setting bullet speed
        fireVelocity.Damage = (int)_damage; // Setting bullet damage
        _currentAmmo -= 1; // counting down 1 bullet less
        if (_bulletAmount > 1)
        {
            float angleAddDirection = 3;
            // Setting fire target into a vector 3 for keeping constant distance value
            for (int i = 1; i < _bulletAmount; i++)
            {
                if (i % 2 == 0)
                {
                    pulledObject = _bulletsPool.PullBullet(); // pulling the bullet from the pool system script
                    pulledObject.transform.position = _fireOrigin.position; // Setting start position for the bullet
                    fireVelocity = pulledObject.GetComponent<FireVelocity>(); // getting the bullet script
                    _targetFireObject.position = _fireTarget.position;
                    _targetFireObject.rotation = _fireTarget.rotation;
                    _fireTargetVector = _targetFireObject.localPosition;
                    _fireTargetVector.z = 22;
                    _fireTargetVector.x += angleAddDirection;
                    _targetFireObject.localPosition = _fireTargetVector;
                    fireVelocity.Bullet_Direction =  (_targetFireObject.position - _fireOrigin.position).normalized; // Setting bullet direction into player's front direction
                    fireVelocity.BulletSpeed = _bulletSpeed; // Setting bullet speed
                    fireVelocity.Damage = (int)_damage; // Setting bullet damage
                    angleAddDirection += angleAddDirection;
                }
                else
                {
                    pulledObject = _bulletsPool.PullBullet(); // pulling the bullet from the pool system script
                    pulledObject.transform.position = _fireOrigin.position; // Setting start position for the bullet
                    fireVelocity = pulledObject.GetComponent<FireVelocity>(); // getting the bullet script
                    _targetFireObject.position = _fireTarget.position;
                    _targetFireObject.rotation = _fireTarget.rotation;
                    _fireTargetVector = _targetFireObject.localPosition;
                    _fireTargetVector.z = 22;
                    _fireTargetVector.x -= angleAddDirection;
                    _targetFireObject.localPosition = _fireTargetVector;
                    fireVelocity.Bullet_Direction =  (_targetFireObject.position - _fireOrigin.position).normalized; // Setting bullet direction into player's front direction
                    fireVelocity.BulletSpeed = _bulletSpeed; // Setting bullet speed
                    fireVelocity.Damage = (int)_damage; // Setting bullet damage
                }
                
            }
        }
    }
    //Sending info out
    public (int, int) GetAmmo()
    {
        return (_currentAmmo, _magazineFullCapacity);
    }

}






