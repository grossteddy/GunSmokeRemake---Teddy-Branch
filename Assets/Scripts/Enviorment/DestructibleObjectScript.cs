using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectScript : MonoBehaviour, IDamageable
{
    [Header("Object Setting")]
    [SerializeField] int hp = 1;
    [SerializeField] int explosiveDmg;
    [SerializeField] float explosiveRadius = 5;
    [SerializeField] bool canExplode = false;

    [SerializeField] float force = 200;
    [SerializeField] float upForce = 0.5f;

    [Header("Dependencies")]
    [SerializeField] Transform Object;
    [SerializeField] Transform ExplodingParts;
    [SerializeField] Material[] damagedMaterials;
    [SerializeField] Material destructMaterial;
    [SerializeField] LayerMask damagingLayer;
    [SerializeField] LayerMask moveLayer;

    PoolSystemEffects explosionFx;
    private Vector3 dir;


    void Start()
    {
        explosionFx = GameObject.Find("ParticleFxPool").GetComponent<PoolSystemEffects>();
        Object = transform.GetChild(0);
        ExplodingParts = transform.GetChild(1);
        ExplodingParts.gameObject.SetActive(false);
        //set another private for the start hp
    }
    private void MakeDamage()
    {

        if (explosionFx != null)
        {
            explosionFx.CallObject(transform.position, 2);
        }
        Collider[] collidersToDamage = Physics.OverlapSphere(transform.position, explosiveRadius, damagingLayer);

        foreach (Collider nearbyObject in collidersToDamage)
        {
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(1, dir, true);
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, explosiveRadius, moveLayer);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, explosiveRadius, upForce, ForceMode.Impulse);
            }
        }
    }
    public void TakeDamage(int damage, Vector3 direction, bool isExplosiveDamage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            DestroyObject(direction);
        }
    }

    private void DestroyObject(Vector3 direction)
    {
        //destructsPool.CallObjectWithDir(this.transform.position, direction, destructMaterial);
        
        Object.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        ExplodingParts.gameObject.SetActive(true);
        if (canExplode)
        {
            MakeDamage();
        }
        //var destructAfterShock = ExplodingParts.gameObject.GetComponent<DestructAfterShock>();
        //destructAfterShock.dirPulse = direction;
    }
}
