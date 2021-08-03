using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class openCircleDoor : MonoBehaviour
{
    public Material openIndicator;

    public bool canOpen;
    public bool charIsClose;


    // Start is called before the first frame update
    void Start()
    {
        ReactionManager.Add("enableDoorButton", EnableDoorButton);
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

    private void Update()
    {
        if (canOpen == true && charIsClose == true && Input.GetKeyDown(KeyCode.E)) { 
            ReactionManager.Call("openCircleDoor");
        }
    }
    private void EnableDoorButton(string[] empty)
    {
        gameObject.GetComponent<Renderer>().material = openIndicator;

        canOpen = true;
        gameObject.tag = "Touch";

    }
}
