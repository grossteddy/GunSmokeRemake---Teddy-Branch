using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteThrow : MonoBehaviour
{
    [Header("settings")]
    [SerializeField] protected float throwtime = 3f;
    [SerializeField] protected float cooldown = 3f;
    protected float cooldownTimer = 0;

    [Header("Dependencies")]
    [SerializeField] protected Transform throwOrigin;
    protected Vector3 throwTarget;
    [SerializeField] protected PoolSystem_Bullets poolDynamite;

    protected void Throw()
    {
        GameObject dynamite = poolDynamite.PullBullet();
        dynamite.transform.position = throwOrigin.position;
        dynamite.transform.rotation = throwOrigin.rotation;
        Vector3 Vo = CalculateVelocity(throwTarget, throwOrigin.position, throwtime);
        Rigidbody rb = dynamite.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vo;
        }
        cooldownTimer = cooldown;
    }

    protected Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        //define the distance x and y first
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        //create a float that represents our distance
        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * MathF.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

}
