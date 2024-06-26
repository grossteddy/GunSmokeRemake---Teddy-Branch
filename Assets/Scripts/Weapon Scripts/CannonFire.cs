using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{
    [SerializeField] Transform cannonBall;
    [SerializeField] Rigidbody rb_Ball;
    [SerializeField] float force;
    Animator anim;
    PoolSystemEffects trailFx;
    float timer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        trailFx = GameObject.Find("BulletTrailFxPool").GetComponent<PoolSystemEffects>();
    }
    void FireCannon()
    {
        trailFx.CallTrailObject(transform.position, 1, cannonBall);
        rb_Ball.AddForce(cannonBall.forward*force, ForceMode.Impulse);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("CannonShot");
        }
    }
}
