using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipFramesSystem : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
    }

}
