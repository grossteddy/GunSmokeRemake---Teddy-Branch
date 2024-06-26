using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    //layerMask for mouse Ray
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Camera _targetCamera;

    //Choose Controller
    [SerializeField] ChooseController _chooseController;

    //Speed Parameters Control
    [FormerlySerializedAs("_movementSpeed")] [SerializeField] float _runSpeed = 1;
    [FormerlySerializedAs("_runspeed")] [SerializeField] float _walkSpeed = 2;
    private float _runSpeedMemory;

    //Movement Parameters
    Transform _playerTransform;
    Vector3 _updatedPos;
    Rigidbody _rbPlayer;

    //Input System
    PlayerInputSystem InputSystem;

    //Rotation Parameters
    Vector3 _rotateTowardsUpdated;
    Vector2 _gamepadFollow;
    Vector2 _mouseWorldPosition;
    Vector2 _aimingDir;
    Transform _targetRotation;
    Transform _playerGfx;
    [SerializeField] float _rotationThreshold = 3;
    Vector3 _targetLockAxis;
    
    //Coliision factors
    private bool _collide;
    private Vector3 _colliderMin;
    private Vector3 _colliderMax;
    private Vector3 _colliderCenter;
    private Vector3 _playerPositionMemory;

    private void Start()
    {
        _runSpeedMemory = _runSpeed;
        _rbPlayer = GetComponent<Rigidbody>();
    }
    void Update()
    {
        SwitchController();
        PlayerRotation_Updated();
        //PlayerMovement_Updated();
    }

    private void FixedUpdate()
    {
        PlayerMovement_RBUpdated();
    }
    enum ChooseController
    {
        MouseAndKeyboard,
        Gamepad,
        OnlyKeyboard,
    }
    public Vector3 GetMouseUIPosition()
    {
        return InputSystem.MousePosition;
    }
    public Vector2 GetMouseWorldPosition()
    {
        return _mouseWorldPosition;
    }
    public RaycastHit GetHitFromRay()
    {
        Ray ray = _targetCamera.ScreenPointToRay(InputSystem.MousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100);
        return hit;
    }
    //Calling all the necceserry objects and scripts
    private void Awake()
    {
        _targetRotation = GameObject.Find("RotateTowards").transform; 
        _playerGfx = GameObject.Find("PlayerGfx").transform;
        InputSystem = GetComponent<PlayerInputSystem>();
        _playerTransform = transform;
        _updatedPos = _playerTransform.position;
        _rotateTowardsUpdated = _targetRotation.position;
    }
 
    void SwitchController()
    {
        switch (_chooseController)
        {
            case ChooseController.MouseAndKeyboard:

                Ray ray = _targetCamera.ScreenPointToRay(InputSystem.MousePosition);
                RaycastHit hit;
                Physics.Raycast(ray, out hit, 100, _layerMask);
                Debug.DrawLine(ray.origin, hit.point, Color.cyan);
                _mouseWorldPosition.x = hit.point.x;
                _mouseWorldPosition.y = hit.point.z;
                _aimingDir = _mouseWorldPosition;

                break;
            case ChooseController.Gamepad:
                _aimingDir = InputSystem.AimDir;
                break;
            case ChooseController.OnlyKeyboard:
                _aimingDir = InputSystem.PlayerMovement;
                break;
        }
    }

    //Move the player as the Vector2 Input instruct and MULTIPLY it to speed, If player pressed Shift so speed equal to run value
    void PlayerMovement_Updated()
    {
        if (InputSystem.RunTrigger)
        {
            _updatedPos.x += InputSystem.PlayerMovement.x * Time.deltaTime * _walkSpeed;
            _updatedPos.z += InputSystem.PlayerMovement.y * Time.deltaTime * _walkSpeed;
        }
        else
        {
            _updatedPos.x += InputSystem.PlayerMovement.x * Time.deltaTime * _runSpeed;
            _updatedPos.z += InputSystem.PlayerMovement.y * Time.deltaTime * _runSpeed;
        }
        _playerTransform.position = _updatedPos;
    }
    void PlayerMovement_RBUpdated()
    {
        //_rbPlayer.position = _updatedPos;
        _updatedPos.x = InputSystem.PlayerMovement.x;
        _updatedPos.z = InputSystem.PlayerMovement.y;
        _updatedPos.y = 0;
        _updatedPos = _updatedPos.normalized * _runSpeed * Time.fixedDeltaTime;
        _rbPlayer.velocity = _updatedPos ;
    }
    
    //Rotate Player Graphic to look at the follow object
    void PlayerRotation_Updated()
    {
        if(_chooseController == ChooseController.MouseAndKeyboard)
        {
            _targetRotation.position = _rotateTowardsUpdated;
            _rotateTowardsUpdated.z = _aimingDir.y;
            _rotateTowardsUpdated.x = _aimingDir.x;
            _playerGfx.LookAt(_targetRotation);
            _targetLockAxis = _playerGfx.rotation.eulerAngles;
            _targetLockAxis.z = 0;
            _targetLockAxis.x = 0;
            _playerGfx.rotation = Quaternion.Euler(_targetLockAxis);
            _targetRotation.rotation = _playerGfx.rotation;
        }
        else
        {
            _targetRotation.localPosition = _rotateTowardsUpdated;
            _rotateTowardsUpdated.z = _aimingDir.y * 5;
            _rotateTowardsUpdated.x = _aimingDir.x * 5;
            _playerGfx.LookAt(_targetRotation);
            
        }
    }
    public void GetSpeedBoost(float boost)
    {
        _runSpeed += boost;
        _walkSpeed += boost;
    }

    public Vector2 GetAimingDir()
    {
        return _aimingDir;
    }


    void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
    }



    void PlayerCollisions()
    {
        
    }
}
