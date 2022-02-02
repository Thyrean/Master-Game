using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{  
    private Animator animator;

    void Start()
    {
        ReactionManager.Add("CapsuleInteraction", MoveCapsule);

        animator = gameObject.GetComponent<Animator>();
    }

    private void MoveCapsule(string[] empty)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        { 
            animator.Play("Capsule_TravelR");
        }
       
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chilling"))
        { 
            animator.Play("Capsule_TravelL");
        }
    }
}
