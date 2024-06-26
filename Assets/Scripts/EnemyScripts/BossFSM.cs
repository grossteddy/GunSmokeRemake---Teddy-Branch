using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFSM : EnemyFSM
{
    [Header("Dependencies")]
    [SerializeField] BossEnemyStates currentState = BossEnemyStates.Idle;
    [SerializeField] Transform player;
    [SerializeField] EnemyFire enemyFireSystem;
    [SerializeField] Animator animator;

    [Header("Settings")]
    [SerializeField] float turnSpeed = 1;

    private void Update()
    {
        ExecuteCurrentState();
    }
    public void SwitchState(BossEnemyStates newState)
    {
        currentState = newState;
    }

    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case BossEnemyStates.Idle:
                IdleState();
                break;

            case BossEnemyStates.Stunn:
                StunnState();
                break;

            case BossEnemyStates.Shoot:
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


    }

    private void StunnState()
    {
        

    }

    private void IdleState()
    {

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

public enum BossEnemyStates { Idle, Stunn, Shoot }
