using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCollider : MonoBehaviour
{
    private WavesManager _wavesManager;
    private Animator thisAnim;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (_wavesManager.CheckCurrentWave())
            {
                print("isTrue");
                thisAnim.SetTrigger("OpenGate");
            }
        }
    }

    void Start()
    {
        thisAnim = GetComponent<Animator>();
        _wavesManager = FindObjectOfType<WavesManager>();
    }
    void Update()
    {

    }
}
