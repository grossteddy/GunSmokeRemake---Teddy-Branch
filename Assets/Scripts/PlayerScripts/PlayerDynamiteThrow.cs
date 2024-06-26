using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDynamiteThrow : DynamiteThrow
{
    [Header("PlayerSettings")]
    [SerializeField] public int numOfDynamite = 1;
    [SerializeField] LayerMask layer;

    [Header("PlayerDependecies")]
    [SerializeField] PlayerInputSystem InputSystem;
    [SerializeField] Camera cam;
    private Animator animator;

    private void Start()
    {
        try
        {
            animator = GetComponentInChildren<Animator>();
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void AddDynamit()
    {
        numOfDynamite++;
    }
    public void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        if (InputSystem.ThrowTrigger && numOfDynamite >= 1 && cooldownTimer <= 0)
        {
            GetTarget();
            animator.SetTrigger("Throw");
            Throw();
            numOfDynamite--;
        }
    }

    private void GetTarget()
    {
        Ray camRay = cam.ScreenPointToRay(InputSystem.MousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            throwTarget = hit.point;
        }
    }

    public int GetNumberOfDynamite()
    {
        return numOfDynamite;
    }

    public void AddDynamite(int number)
    {
        numOfDynamite += number;
    }
}
