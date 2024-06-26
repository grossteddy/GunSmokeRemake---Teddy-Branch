using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LookForPlayer : MonoBehaviour
{
    
    [Header("Setting")]
    [SerializeField] public float AggroRange;
    [SerializeField] float shootingRange;
    [SerializeField] LayerMask blockVisionMask;

    [Header("Dependencies")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform player;
    private RaycastHit hit;

    private void Start()
    {
        try
        {
            player = GameObject.Find("Player").transform.Find("CenterMass");
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public bool CheckIfPlayerInRange()
    {
        return Physics.CheckSphere(transform.position, AggroRange, playerMask);
    }

    public bool CheckShootingRange()
    {
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, shootingRange, blockVisionMask))
        {
            return hit.transform.CompareTag("Player");
            /*if (hit.collider.CompareTag("Player"))
            {
                return true;
            }*/
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AggroRange);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, (player.position - transform.position).normalized * shootingRange, Color.red);
    }
}
