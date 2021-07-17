using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class circleDoorInteract : MonoBehaviour
{
    private Animator animator;

    private void OnMouseDown()
    {
        ReactionManager.Call("rotateDoor");
        //this.transform.Rotate(Vector3.forward * 30);
    }
}

