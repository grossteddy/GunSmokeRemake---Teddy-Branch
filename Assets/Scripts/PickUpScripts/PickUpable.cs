using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpable : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    [SerializeField] protected AudioClip[] OnPickUpSound;
    [SerializeField] protected AudioSource audioSource;


    private void Start()
    {
        try
        {
            player = GameObject.Find("Player");
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    protected void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            for (int i = 0; i < OnPickUpSound.Length; i++)
            {
                if (OnPickUpSound[i] != null)
                {
                    audioSource.PlayOneShot(OnPickUpSound[i]);
                }
            }
            OnPickUp();
        }
    }

    protected abstract void OnPickUp();

}
