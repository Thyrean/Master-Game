using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class rotateDoor : MonoBehaviour
{
    public GameObject[] doorParts;

    private Animator animator;

    public bool canAnimate;
    public bool charIsClose;

    void Start()
    {
        charIsClose = false;

        animator = gameObject.GetComponent<Animator>();

        ReactionManager.Add("rotateDoor", RotateDoor);
        ReactionManager.Add("openCircleDoor", OpenCircleDoor);
    }

    private void Update()
    {
        if (doorParts[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("90")
            && doorParts[1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("180")
            && doorParts[2].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("270"))
        {
            ReactionManager.Call("enableDoorButton");
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha4) && charIsClose == true)
        {
            ReactionManager.Call("rotateDoor");
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
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

    private void RotateDoor(string[] empty)
    {
        for (var i = 0; i < doorParts.Length; i++)
        {
            if (doorParts[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("0"))
                doorParts[i].GetComponent<Animator>().Play("rotateTo90");

            else if (doorParts[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("90"))
                doorParts[i].GetComponent<Animator>().Play("rotateTo180");

            else if (doorParts[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("180"))
                doorParts[i].GetComponent<Animator>().Play("rotateTo270");

            else if (doorParts[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("270"))
                doorParts[i].GetComponent<Animator>().Play("rotateTo360");
        }
    }

    private void OpenCircleDoor(string[] empty)
    {
        animator.Play("openCircleDoor");

        StartCoroutine(DestroyDoor(doorParts[0]));
        StartCoroutine(DestroyDoor(doorParts[1]));
        StartCoroutine(DestroyDoor(doorParts[2]));
        StartCoroutine(DestroyDoor(doorParts[3]));
    }

    IEnumerator DestroyDoor(GameObject go)
    {
        yield return new WaitForSeconds(2);

        go.SetActive(false);
    }
}

