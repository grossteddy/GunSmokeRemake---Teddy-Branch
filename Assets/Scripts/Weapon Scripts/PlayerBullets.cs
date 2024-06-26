using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : FireVelocity
{
    private GeneralEnemyScript enemyHit;
    PoolSystemEffects explosionFx;
    PoolSystemEffects trailFx;

    private Vector3 dir;

    private void Start()
    {
        explosionFx = GameObject.Find("ParticleFxPool").GetComponent<PoolSystemEffects>();
        //trailFx = GameObject.Find("BulletTrailFxPool").GetComponent<PoolSystemEffects>(); 
    }
    private void OnEnable()
    {
        /*if (trailFx != null)
        {
            trailFx.CallTrailObject(transform.position, 1, transform);
        }*/
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            explosionFx.CallObject(transform.position, 0.5f);
            try
            {
                enemyHit = collision.GetComponent<GeneralEnemyScript>();
            }
            catch (System.Exception)
            {

                throw;
            }
            enemyHit.ImpactForce(Bullet_Direction);
            enemyHit.TakeDamage(Damage, dir, false);
            
            this.gameObject.SetActive(false);
        }
        if (collision.CompareTag("Destructable"))
        {
            explosionFx.CallObject(transform.position, 0.5f);
            var destructScript = collision.GetComponent<DestructibleObjectScript>();
            destructScript.TakeDamage(1, Bullet_Direction, false);
            this.gameObject.SetActive(false);
            print("HitBarrel");
        }
    }
}
