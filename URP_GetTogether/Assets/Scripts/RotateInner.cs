using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RotateInner : MonoBehaviour
{
    private Animator animator;
    public bool canAnimate;

    void Start()
    {
        ReactionManager.Add("rotateInner", rotateInner);
    }

    private void OnMouseDown()
    {
        ReactionManager.Call("rotateInner");
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    private void rotateInner(string[] empty)
    {
        canAnimate = true;

        animator = gameObject.GetComponent<Animator>();

        if (canAnimate == true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("0"))
                animator.Play("rotateTo90");

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("90"))
                animator.Play("rotateTo180");

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("180"))
                animator.Play("rotateTo270");

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("270"))
                animator.Play("rotateTo360");

            canAnimate = false;
        }
        else return;

    }
}
