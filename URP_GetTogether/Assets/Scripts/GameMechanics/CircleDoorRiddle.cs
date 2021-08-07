using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CircleDoorRiddle : MonoBehaviour
{
    public GameObject[] doorParts;

    public bool canAnimate;
    public bool charIsClose;

    public bool riddleSolved;
    public bool screenDisable;

    public string[] correctDegrees;

    public GameObject connectedScreen;

    void Start()
    {
        charIsClose = false;
        riddleSolved = false;
        screenDisable = false;

        ReactionManager.Add("rotateDoor", RotateDoor);
        ReactionManager.Add("EnableDoorScreen", EnableDoorScreen);
    }

    private void Update()
    {
        if (doorParts[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(correctDegrees[0])
            && doorParts[1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(correctDegrees[1])
            && doorParts[2].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(correctDegrees[2])
            && riddleSolved == false && screenDisable == false)
        {
            riddleSolved = true;
            ReactionManager.Call("EnableDoorScreen");
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

    private void EnableDoorScreen(string[] empty)
    {
        if (riddleSolved == true && screenDisable == false)
        {
            connectedScreen.SetActive(true);
            screenDisable = true;
        }
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("rotateDoor", RotateDoor);
        ReactionManager.Remove("EnableDoorScreen", EnableDoorScreen);
    }
}

