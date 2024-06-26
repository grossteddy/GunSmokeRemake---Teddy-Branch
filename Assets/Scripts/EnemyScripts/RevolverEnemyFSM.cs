using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class RevolverEnemyFSM : EnemyFSM
{
    [Header("Dependencies")]
    [SerializeField] RevolverEnemyStates currentState = RevolverEnemyStates.Idle;
    [SerializeField] LookForPlayer lookForPlayer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] EnemyFire enemyFireSystem;
    [SerializeField] NavMeshObstacle obstacleSystem;
    [SerializeField] Animator animator;

    [Header("Settings")]
    [SerializeField] bool chasePlayerOnAwake = false;
    [SerializeField] float turnSpeed = 1;

    private void Start()
    {
        try
        {
            animator = this.GetComponentInChildren<Animator>();
            obstacleSystem = this.GetComponentInChildren<NavMeshObstacle>();
            agent = this.GetComponent<NavMeshAgent>();
        }
        catch (System.Exception)
        {

            throw;
        }

        if (chasePlayerOnAwake)
        {
            currentState = RevolverEnemyStates.Chase;
        }
    }

    private void Update()
    {
        ExecuteCurrentState();
    }
    private void SwitchState(RevolverEnemyStates newState)
    {
        currentState = newState;
    }

    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case RevolverEnemyStates.Idle:
                IdleSearchState();
                break;

            case RevolverEnemyStates.Chase:
                ChaseState();
                break;

            case RevolverEnemyStates.Shoot:
                ShootingStage();
                break;

            default:

                break;
        }
    }

    private void ShootingStage()
    {
        animator.SetBool("Shooting", true);
        TurnToPlayer();
        obstacleSystem.enabled = true;
        enemyFireSystem.EnemyFireController();

        if (!lookForPlayer.CheckShootingRange())
        {
            obstacleSystem.enabled = false;
            currentState = RevolverEnemyStates.Chase;
        }
    }

    private void ChaseState()
    {
        animator.SetBool("Shooting", false);
        TurnToPlayer();
        if (lookForPlayer.CheckShootingRange())
        {
            agent.destination = transform.position;
            currentState = RevolverEnemyStates.Shoot;
        }
        else
        {
            agent.destination = player.position;
        }
    }

    private void IdleSearchState()
    {
        animator.SetBool("Shooting", false);
        if (lookForPlayer.CheckIfPlayerInRange())
        {
            SwitchState(RevolverEnemyStates.Chase);
        }
    }

    private void TurnToPlayer()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = player.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}

public enum RevolverEnemyStates{Idle, Chase, Shoot}
