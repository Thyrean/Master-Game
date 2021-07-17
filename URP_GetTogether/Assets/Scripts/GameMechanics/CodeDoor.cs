using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeDoor : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        ReactionManager.Add("OpenCodeDoor", OpenDoor);

        animator = gameObject.GetComponent<Animator>();
    }

    private void OpenDoor(string[] empty)
    {
        animator.Play("Door_Open");

        //animator.Play("door_3_open");
    }
}
