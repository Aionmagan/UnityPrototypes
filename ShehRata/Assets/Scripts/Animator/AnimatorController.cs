using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator animator; 

    int hashOnJump;
    int hashAnalog;
    int hashOnGrab;
    int hashOnRun; 

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();

        hashOnJump = Animator.StringToHash("OnJump");
        hashAnalog = Animator.StringToHash("Axis");
        hashOnGrab = Animator.StringToHash("IsGrab");
        hashOnRun  = Animator.StringToHash("RunAxis");
    }

    public void AnimMove(float speed, float isRunning)
    {
        animator.SetFloat(hashAnalog, speed);
        animator.SetFloat(hashOnRun, isRunning);
    }

    public void AnimGrab(bool grab)
    {
        animator.SetBool(hashOnGrab, grab);
    }

    public void AnimJump()
    {
        animator.SetTrigger(hashOnJump);
    }
    
}
