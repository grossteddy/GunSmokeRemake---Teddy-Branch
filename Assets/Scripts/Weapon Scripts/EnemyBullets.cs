using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : FireVelocity
{
    PlayerStats _playerStats;
    [SerializeField] int damage;
    PoolSystemEffects poolFx;

    private Vector3 dir;

    private void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        poolFx = GameObject.Find("ParticleFxPool").GetComponent<PoolSystemEffects>();
    }
    private void OnTriggerEnter(Collider player)
    {
        
        if (player.CompareTag("Player"))
        {
            poolFx.CallObject(transform.position, 0.5f);
            _playerStats.TakeDamage(damage,dir,false);
            this.gameObject.SetActive(false);
        }
    }
}
