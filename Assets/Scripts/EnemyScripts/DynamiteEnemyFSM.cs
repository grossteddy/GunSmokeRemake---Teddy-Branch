using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DynamiteEnemyFSM : EnemyFSM
{
    [Header("Dependencies")]
    [SerializeField] DynamiteEnemyStates currentState = DynamiteEnemyStates.Idle;
    [SerializeField] LookForPlayer lookForPlayer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] EnemyDynamiteThrow enemyDynamiteThrow;
    [SerializeField] NavMeshObstacle obstacleSystem;

    [Header("Settings")]
    [SerializeField] bool chasePlayerOnAwake = false;
    [SerializeField] float turnSpeed = 1;

    private void Start()
    {
        try
        {
            obstacleSystem = this.GetComponentInChildren<NavMeshObstacle>();
            agent = this.GetComponent<NavMeshAgent>();
        }
        catch (System.Exception)
        {

            throw;
        }

        if (chasePlayerOnAwake)
        {
            currentState = DynamiteEnemyStates.Chase;
        }
    }

    private void Update()
    {
        ExecuteCurrentState();
    }
    private void SwitchState(DynamiteEnemyStates newState)
    {
        currentState = newState;
    }

    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case DynamiteEnemyStates.Idle:
                IdleSearchState();
                break;

            case DynamiteEnemyStates.Chase:
                ChaseState();
                break;

            case DynamiteEnemyStates.Throw:
                ThrowingStage();
                break;

            default:

                break;
        }
    }

    private void ThrowingStage()
    {
        TurnToPlayer();
        obstacleSystem.enabled = true;
        enemyDynamiteThrow.EnemyThrow();

        if (!lookForPlayer.CheckShootingRange())
        {
            obstacleSystem.enabled = false;
            currentState = DynamiteEnemyStates.Chase;
        }
    }

    private void ChaseState()
    {
        TurnToPlayer();
        if (lookForPlayer.CheckShootingRange())
        {
            agent.destination = transform.position;
            currentState = DynamiteEnemyStates.Throw;
        }
        else
        {
            agent.destination = player.position;
        }
    }

    private void IdleSearchState()
    {
        if (lookForPlayer.CheckIfPlayerInRange())
        {
            SwitchState(DynamiteEnemyStates.Chase);
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

    public enum DynamiteEnemyStates { Idle, Chase, Throw }
}
