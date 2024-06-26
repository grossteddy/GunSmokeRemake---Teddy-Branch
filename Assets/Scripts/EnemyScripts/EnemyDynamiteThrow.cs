using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDynamiteThrow : DynamiteThrow
{
    [SerializeField] Transform player;

    private Animator animator;

    private void Start()
    {
        try
        {
            animator = GetComponentInChildren<Animator>();
            player = GameObject.Find("Player").transform;
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void EnemyThrow()
    {
        throwTarget = player.position;

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (cooldownTimer <= 0)
        {
            animator.SetTrigger("Throw");
            Throw();
        }
    }
}
