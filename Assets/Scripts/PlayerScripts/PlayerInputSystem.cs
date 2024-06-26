using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    //New Input System Configurations
    IAC_GunSmokeRemake PlayerInputController;
    InputAction _move;
    InputAction _fire;
    InputAction _run;
    InputAction _aim;
    InputAction _mousePos;
    InputAction _throw;

    //Outputs Rotation Data
    public bool PressedRot;

    //Outputs Movement&Fire Data
    public Vector2 PlayerMovement;
    [HideInInspector] public Vector2 AimDir;
    [HideInInspector] public Vector2 MousePosition;
    [HideInInspector] public bool FireTrigger;
    [HideInInspector] public bool RunTrigger;
    [HideInInspector] public bool ThrowTrigger;

    private void Awake()
    {
        PlayerInputController = new IAC_GunSmokeRemake();
    }
    private void OnEnable()
    {
        _fire = PlayerInputController.Player.Fire;
        _throw = PlayerInputController.Player.DynamiteThrow;
        _move = PlayerInputController.Player.Move;
        _run = PlayerInputController.Player.Run;
        _aim = PlayerInputController.Player.Aim;
        _mousePos = PlayerInputController.Player.MousePosition;

        _mousePos.Enable();
        _aim.Enable();
        _move.Enable();
        _fire.Enable();
        _throw.Enable();
        _run.Enable();
    }

    private void OnDisable()
    {
        _mousePos.Disable();
        _move.Disable();
        _fire.Disable();
        _throw.Disable();
        _run.Disable();
        _aim.Disable();
    }

 
    void Update()
    {
        MousePosition = _mousePos.ReadValue<Vector2>();
        PressedRot = _move.triggered;
        PlayerMovement = _move.ReadValue<Vector2>();
        FireTrigger = _fire.ReadValue<float>() > 0.1;
        ThrowTrigger = _throw.ReadValue<float>() > 0.1;
        RunTrigger = _run.ReadValue<float>() > 0.1;
        AimDir = _aim.ReadValue<Vector2>();
        
    }
}
