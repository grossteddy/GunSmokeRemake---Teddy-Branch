using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamiteScript : MonoBehaviour
{
    [Header("Explostion Settings")]
    [SerializeField] float delay = 6f;
    [SerializeField] float blastRadius = 15f;
    [SerializeField] int damage = 1;
    [SerializeField] float force = 200;
    [SerializeField] float upForce = 0.5f;
    [SerializeField] float effectSize = 1;
    [SerializeField] LayerMask damageLayer;
    [SerializeField] LayerMask moveLayer;

    [Header("Dependencies")]
    [SerializeField]PoolSystemEffects poolFx;

    private float countdown = 0;
    private bool hasExploded = false;

    private Vector3 dir;

    // Start is called before the first frame update
    void OnEnable()
    {
        countdown = delay;
        hasExploded = false;
    }

    private void Start()
    {
        poolFx = GameObject.Find("ParticleFxPool").GetComponent<PoolSystemEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
            this.gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        if (poolFx != null)
        {
            poolFx.CallObject(transform.position, effectSize);
        }
        Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, blastRadius, damageLayer);

        foreach(Collider nearbyObject in collidersToDamage)
        {
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.TakeDamage(damage, dir, true);
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius, moveLayer);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, blastRadius, upForce, ForceMode.Impulse);
            }
        }
    }
}
