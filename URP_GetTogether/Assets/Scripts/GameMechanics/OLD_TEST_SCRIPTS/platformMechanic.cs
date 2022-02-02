using System.Collections;
using Assets.Scripts.ActionReactionSystem;
using System.Collections.Generic;
using UnityEngine;

public class platformMechanic : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        ReactionManager.Add("moveRed", MoveRed);
        ReactionManager.Add("moveBlue", MoveBlue);
        ReactionManager.Add("moveGreen", MoveGreen);
    }

    private void MoveRed(string[] empty)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && gameObject.tag == "redPlatform")
            animator.Play("moveRed");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleGreen"))
            animator.Play("returnGreen");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleBlue"))
            animator.Play("returnBlue");
    }

    private void MoveBlue(string[] empty)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && gameObject.tag == "bluePlatform")
            animator.Play("moveBlue");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleGreen"))
            animator.Play("returnGreen");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleRed"))
            animator.Play("returnRed");
    }

    private void MoveGreen(string[] empty)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && gameObject.tag == "greenPlatform")
            animator.Play("moveGreen");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleRed"))
            animator.Play("returnRed");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idleBlue"))
            animator.Play("returnBlue");
    }
}