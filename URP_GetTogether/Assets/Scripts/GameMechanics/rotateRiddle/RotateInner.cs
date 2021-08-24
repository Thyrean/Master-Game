using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RotateInner : MonoBehaviour
{
    private Animator animator;
    public bool canAnimate;

    public bool charIsClose;

    void Start()
    {
        charIsClose = false;
        ReactionManager.Add("rotateInner", rotateInner);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && charIsClose == true)
        {
            ReactionManager.Call("rotateInner");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            charIsClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            charIsClose = false;
        }
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

    private void OnDestroy()
    {
        ReactionManager.Remove("rotateInner", rotateInner);
    }
}
