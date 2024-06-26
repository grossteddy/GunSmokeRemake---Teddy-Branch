using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FireVelocity : MonoBehaviour
{
    // Speed value and direction is set when bullet called
    [HideInInspector] public float BulletSpeed;
    [HideInInspector] public Vector3 Bullet_Direction;
    [HideInInspector] public int Damage;

    //Bullet Movement calculation
    void BulletDirection()
    {
        transform.Translate(Bullet_Direction * Time.deltaTime * BulletSpeed);
    }
    void Update()
    {
        BulletDirection();
    }
}
