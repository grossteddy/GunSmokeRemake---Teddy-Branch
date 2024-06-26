using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _rigidness;
    [SerializeField] Vector3 _offsetPosition;
    private Vector3 targetShake;
    private Vector3 originShake;
    Vector3 _updatedPos;
    float tFactor = 2;
    [SerializeField] private float speedOfShake;
    private float tFactorShake = 0;
    private float tFactorShakeRev = 0;
    private bool shakeNow;
    [SerializeField] private float shakeFirAmout;
    [SerializeField] private Transform Towards;

    
    Vector3 _playerPosition;
    
    void Start()
    {
        _updatedPos = _playerTransform.position;
        _updatedPos.y += _offsetPosition.y;
        _updatedPos.z += _offsetPosition.z;
    }

    void FollowFunction()
    {
        _updatedPos = Vector3.Lerp(_updatedPos, _playerPosition + _offsetPosition, _rigidness * Time.deltaTime);
    }

    public void ShakeCamera()
    {
        shakeNow = true;
        originShake = transform.position;
        //targetShake = transform.position - ((_playerTransform.forward * shakeFirAmout).normalized);
        targetShake = transform.position + Random.insideUnitSphere * shakeFirAmout;
        tFactorShake = 0;
        tFactorShakeRev = 0;
    }
    public void ShakeCameraUpdate()
    {
        originShake = transform.position;
        if (tFactorShakeRev > 1)
        {
            shakeNow = false;
        }
        if (tFactorShake < 1)
        {
            transform.localPosition = Vector3.Slerp(originShake, targetShake, tFactorShake += Time.deltaTime * speedOfShake);
        }
        else if (tFactorShakeRev < 1)
        {
            transform.localPosition = Vector3.Slerp(targetShake, originShake, tFactorShakeRev += Time.deltaTime * speedOfShake);
        }
    }
    void LateUpdate()
    {
        
        transform.position = _updatedPos;
        _playerPosition = _playerTransform.position;
        FollowFunction();
        if (shakeNow)
        {
            ShakeCameraUpdate();
        }
    }
}
