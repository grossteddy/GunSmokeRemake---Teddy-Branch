using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class RifleEnemyFSM : EnemyFSM
{
    [Header("Dependencies")]
    [SerializeField] RevolverEnemyStates currentState = RevolverEnemyStates.Idle;
    [SerializeField] LookForPlayer lookForPlayer;
    [SerializeField] Transform player;
    [SerializeField] EnemyFire enemyFireSystem;

    [Header("Settings")]
    [SerializeField] float turnSpeed = 1;


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

            case RevolverEnemyStates.Shoot:
                ShootingStage();
                break;

            default:

                break;
        }
    }

    private void ShootingStage()
    {
        TurnToPlayer();
        enemyFireSystem.EnemyFireController();

        if (!lookForPlayer.CheckShootingRange())
        {
            currentState = RevolverEnemyStates.Idle;
        }
    }

    private void IdleSearchState()
    {
        if (lookForPlayer.CheckShootingRange())
        {
            SwitchState(RevolverEnemyStates.Shoot);
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

public enum RifleEnemyStates{Idle, Shoot}
