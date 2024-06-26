using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public abstract class GeneralEnemyScript : MonoBehaviour, IDamageable
{
    [Header("Basic Enemy Settings")]
    [SerializeField] protected int _hp = 1;
    [SerializeField] bool _hasInvuln = false;
    [SerializeField] float _vulnTimer = 6;
    [SerializeField] EnemyAnimationTree _enemyAnimationScript;
    [SerializeField] Rigidbody _rb;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] protected Animator _animator;

    [Header("Dependecies")]
    [SerializeField] protected EnemyFSM _enemyFSM;
    [SerializeField] PoolSystem_Bullets poolPickup;

    private Vector3 dir;
    private List<Collider> ragdollParts = new List<Collider>();
    private List<Rigidbody> ragdollRigid = new List<Rigidbody>();

    private void Start()
    {
        SetRagdollParts();
        if (_enemyFSM == null)
        {
            try
            {
                _animator = GetComponentInChildren<Animator>();
                _enemyFSM = GetComponent<EnemyFSM>();
                _navMeshAgent = GetComponent<NavMeshAgent>();

            }
            catch (Exception)
            {

                throw;
            }
        }

        if(_animator == null)
        {
            _animator = this.GetComponentInChildren<Animator>();
        }
    }

    private void Awake()
    {
        SetRagdollParts();
    }

    private void OnEnable()
    {
        BoxCollider thisBoxCollider = this.gameObject.GetComponent<BoxCollider>();
        SphereCollider thisSphereCollider = this.gameObject.GetComponent<SphereCollider>();

        if (thisBoxCollider != null)
        {
            thisBoxCollider.enabled = true;
        }
        if (thisSphereCollider != null)
        {
            thisSphereCollider.enabled = true;
        }

        _animator.enabled = true;

        foreach (Collider collider in ragdollParts)
        {
            collider.isTrigger = true;
        }
    }

    private void SetRagdollParts()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            if(collider.gameObject != this.gameObject)
            {
                collider.isTrigger = true;
                ragdollParts.Add(collider);

                Rigidbody rigidbody = collider.gameObject.GetComponent<Rigidbody>();
                if(rigidbody != null)
                {
                    rigidbody.isKinematic = true;
                    ragdollRigid.Add(rigidbody);
                }
            }
        }
    }

    private void TurnOnRagdoll()
    {
        if(_rb.useGravity == true)
        {
            _rb.useGravity = false;
        }

        BoxCollider thisBoxCollider = this.gameObject.GetComponent<BoxCollider>();
        SphereCollider thisSphereCollider = this.gameObject.GetComponent<SphereCollider>();

        if(thisBoxCollider != null)
        {
            thisBoxCollider.enabled = false;
        }
        if (thisSphereCollider != null)
        {
            thisSphereCollider.enabled = false;
        }

        foreach (Collider collider in ragdollParts)
        {
            collider.isTrigger = false;
            collider.attachedRigidbody.velocity = Vector3.zero;
        }

        foreach (Rigidbody rigid in ragdollRigid)
        {
            rigid.isKinematic = false;
        }

        _animator.enabled = false;
    }

    //Health & Damage Functions
    protected void SetHealth(int health)
    {
        _hp = health;
    }
    protected int GetHealth()
    {
        return _hp;
    }
    public void TakeDamage(int damage, Vector3 dir, bool isExplosiveDamage)
    {
        _hp -= damage;
        if(_hp <= 0)
        {
            DropPickUp();
            Death(isExplosiveDamage);
        }

    }

    private void DropPickUp()
    {
        if (poolPickup != null)
        {
            GameObject newPickup = poolPickup.PullBullet();
            newPickup.transform.position = this.transform.position;
        }
    }

    public void ImpactForce(Vector3 dir)
    {
        _rb.AddForce(dir*10, ForceMode.Impulse);
    }
    //If Bullet Hits the enemy use TakeDamage function and set bullet off
    /*protected void OnTriggerEnter(Collider bullet)
    {
        if (bullet.CompareTag("Bullet"))
        {
            bullet.gameObject.SetActive(false);
            TakeDamage(bullet.gameObject.GetComponent<FireVelocity>().Damage);
        }
    }*/
 
    protected void Death(bool isExplosiveDamage)
    {
        _enemyFSM.enabled = false;
        if (_navMeshAgent != null)
        {
            _navMeshAgent.enabled = false;
        }
        _enemyAnimationScript._isDead = true;
        if (isExplosiveDamage)
        {
            TurnOnRagdoll();
        }
    }
}
