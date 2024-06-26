using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [Header("Stats & Settings")]
    [SerializeField] public int HP = 5;
    [SerializeField] float money = 0;
    [SerializeField] bool hasInvuln = true;
    [SerializeField] public float invulnDuration = 4;
    [SerializeField] PostProccessingFX _postFX;
    [SerializeField] PlayerFollow _playerFollow;


    [Header("Dependencies")]
    [SerializeField] PlayerMovement playerMovement;

    private float invulnTimer = 0;
    private bool isInvuln = false;


    PlayerAnimation _playerAnim;
    private void Start()
    {
        try
        {
            playerMovement = this.GetComponent<PlayerMovement>();
            _playerAnim = GetComponent<PlayerAnimation>();
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    private void Update()
    {
        CheckIfInvulnerable();
    }

    public void TakeDamage(int damage, Vector3 dir, bool isExplosiveDamage)
    {
        if (!isInvuln)
        {
            _playerFollow.ShakeCamera();
            _postFX.StartVigAnim();
            _playerAnim.StartFlicker();
            if (HP > 0)
            {
                HP -= damage;
            }
            if (HP <= 0)
            {
                Death();
            }

            else if (hasInvuln)
            {
                isInvuln = true;
            }
        }
    }

    public void AddCash(float cash)
    {
        money += cash;
    }

    public void GetSpeedUpgrade(float speedBoost)
    {
        playerMovement.GetSpeedBoost(speedBoost);
    }

    public float GetMoney()
    {
        return money;
    }

    public float GetHealth()
    {
        return HP;
    }

    private void Death()
    {
        //add death 
    }

    private void CheckIfInvulnerable()
    {
        invulnTimer += Time.deltaTime;

        if (invulnTimer >= invulnDuration)
        {
            invulnTimer = 0;
            isInvuln = false;
        }
    }
}
