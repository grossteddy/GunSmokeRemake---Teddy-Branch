using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Flickering Effect")]
    float _timer;
    float _generalTimer = 50;
    [SerializeField] float _FlickerRythem;
    int _timesOfFlicker = 25;
    [SerializeField] Transform playerGraphics;
    [SerializeField] List<Renderer> thisRenderer = new List<Renderer>();
    [SerializeField] List<Renderer> thisRenderer02 = new List<Renderer>();
    //WeaponHandle
    int weaponFlag;
    [Header("Player General")]
    [SerializeField] GameObject _revolver;
    PlayerInputSystem _playerInput;
    PlayerStats _playerStats;
    PlayerFire _playerFire;
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _playerGfx;
    [Header("Player Animator")]
    Animator _anim;
    [Header("Player Movement Referance")]
    [SerializeField] Transform _movementRef;
    [SerializeField] Transform _rotateRef;
    [SerializeField] Vector3 CordinatotRef;
    [SerializeField] Vector3 RoatationRef;
    Vector3 refPos;
    


    float speed;
    float sideSpeed;
    void Start()
    {
        _playerInput = FindObjectOfType<PlayerInputSystem>();
        _anim = GetComponentInChildren<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        _playerFire = GetComponent<PlayerFire>();
        weaponFlag = _playerFire.chooseWeapon;
        //Fetching all player mesh renderers
        for (int i = 0; i < playerGraphics.childCount; i++)
        {
            var renderer = playerGraphics.GetChild(i).gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                thisRenderer.Add(renderer);
            }
        }
    }

    
    void Update()
    {
        FlickeringWhenHit();
        MovementToAnimationSystem();
        if(weaponFlag != _playerFire.chooseWeapon)
        {
            WeaponSwitch();
        }
    }

    private void WeaponSwitch()
    {
        if (_playerFire.chooseWeapon == 0)
        {
            _revolver.SetActive(false);
            _anim.SetBool("Pistol", false);
        }
        if (_playerFire.chooseWeapon == 1)
        {
            _revolver.SetActive(true);
            _anim.SetBool("Pistol", true);
        }
        weaponFlag = _playerFire.chooseWeapon;
    }

    private void MovementToAnimationSystem()
    {
        if (_playerInput.RunTrigger)
        {
            speed = _playerInput.PlayerMovement.y * 2;
            sideSpeed = _playerInput.PlayerMovement.x * 2;
        }
        else
        {
            speed = _playerInput.PlayerMovement.y;
            sideSpeed = _playerInput.PlayerMovement.x;
        }
        refPos.x = sideSpeed;
        refPos.z = speed;
        refPos.y = 0;
        _movementRef.localPosition = refPos;
        _anim.SetFloat("Speed", CordinatotRef.z);
        _anim.SetFloat("sideSpeed", CordinatotRef.x);
        RoatationRef.y = _playerGfx.eulerAngles.y * -1;
        CordinatotRef.x = _playerTransform.InverseTransformPoint(_movementRef.position).x * 4;
        CordinatotRef.z = _playerTransform.InverseTransformPoint(_movementRef.position).z * 4;
        _rotateRef.eulerAngles = RoatationRef;
    }

    public void StartFlicker()
    {
        _timesOfFlicker = 0;
        _generalTimer = 0;
    }
    private void FlickeringWhenHit()
    {
        _generalTimer += Time.deltaTime;
        _timer += Time.deltaTime;
        if (_timer >= _FlickerRythem)
        {
            _timer = 0;
            if (thisRenderer[0].enabled)
            {
                for (int i = 0; i < thisRenderer.Count; i++)
                {
                    thisRenderer[i].enabled = false;
                }
                for (int i = 0; i < thisRenderer02.Count; i++)
                {
                    thisRenderer02[i].enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < thisRenderer.Count; i++)
                {

                    thisRenderer[i].enabled = true;
                }
                for (int i = 0; i < thisRenderer02.Count; i++)
                {

                    thisRenderer02[i].enabled = true;
                }
            }
        }
        if (_generalTimer > _playerStats.invulnDuration)
        {
            for (int i = 0; i < thisRenderer.Count; i++)
            {
                thisRenderer[i].enabled = true;
            }
            for (int i = 0; i < thisRenderer02.Count; i++)
            {
                thisRenderer02[i].enabled = true;
            }
        }
    }

}
