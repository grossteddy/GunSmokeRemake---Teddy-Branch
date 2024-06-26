using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldFrameSystem : MonoBehaviour
{
    Animator anim;
    [SerializeField] int framesToHold = 8;
    int nextPlayedFrame = 1;
    float currentFrame = 1;
    
    AnimatorStateInfo stateInfo;
    string stateName;


    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    private void Update()
    {
        //TestMethod();
        CheckCurrentFrame();
        CheckTotalFrames();
        //print(CheckCurrentClipName());
    }
    void FixedUpdate()
    {
        HoldFrameSystemMethod();
        ResetNextFrameValueIntoLoop();
    }
    void TestMethod()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            anim.Play(CheckCurrentClipName(), -1, 0.5f);
        }
    }
    private int CheckCurrentFrame()
    {
        float framesAmount;
        framesAmount = CheckTotalFrames() * (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1);
        return (int)framesAmount;
    }

    private int CheckTotalFrames()
    {
        float totalFrames = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length * anim.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;
        return (int)totalFrames;
    }
    private float CheckNormalizeTime()
    {
        float normalizeTime;
        normalizeTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return normalizeTime;
    }

    private void HoldFrameSystemMethod()
    {
        print($"current Frame is {currentFrame} out of Total {CheckTotalFrames()} Frames, Next Played Frame is {nextPlayedFrame}");
        currentFrame += 1f;
        if (currentFrame < nextPlayedFrame)
        {anim.speed = 0;}
        else
        {
            anim.Play("IdleToMove", 0, currentFrame / CheckTotalFrames());
            anim.speed = 1;
            nextPlayedFrame += framesToHold;
        }
    }

    private string CheckCurrentClipName()
    {
        //return anim.GetCurrentAnimatorClipInfo(0)[0].clip.name.ToString();
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        stateName = stateInfo.shortNameHash.ToString();
        return stateName;
    }

    private void ResetNextFrameValueIntoLoop()
    {
        if (nextPlayedFrame > CheckTotalFrames())
        {
            nextPlayedFrame -= CheckTotalFrames();
            currentFrame = 1;
            anim.Play("IdleToMove", 0, 0);
        }
    }

    
}
