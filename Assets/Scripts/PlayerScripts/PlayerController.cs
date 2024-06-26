using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //New Input System Configurations
    IAC_GunSmokeRemake PlayerInputController;
    InputAction _move;
    InputAction _fire;

    //Outputs Data
    public Vector2 PlayerMovement;
    public bool FireTrigger;

    private void Awake()
    {
        PlayerInputController = new IAC_GunSmokeRemake();
    }
    private void OnEnable()
    {
        _fire = PlayerInputController.Player.Fire;
        _move = PlayerInputController.Player.Move;
        _move.Enable();
        _fire.Enable();
        
    }

    private void OnDisable()
    {
        _move.Disable();
        _fire.Disable();
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement = _move.ReadValue<Vector2>();
        FireTrigger = _fire.ReadValue<float>()>0;
    }
}
