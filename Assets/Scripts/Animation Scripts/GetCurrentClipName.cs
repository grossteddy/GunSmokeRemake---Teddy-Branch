using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentClipName : MonoBehaviour
{
    private Animator animator;
    private string currentClipName;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get the current state info
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Get the name of the current clip playing
        currentClipName = stateInfo.IsName("BlendTreeName") ? animator.GetBool("IsSecondClip") ? "SecondClipName" : "FirstClipName" : stateInfo.shortNameHash.ToString();

        // Print the current clip name to the console
        Debug.Log("Current clip name: " + currentClipName);
    }
}
