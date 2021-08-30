using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;

using UnityEngine;

public class orbPlacemen : MonoBehaviour
{
    public bool charHasOrb = false;

    public bool firstOrbPlaced;
    public bool secondOrbPlaced;
    public bool charIsClose;

    public PickUpObject[] playerScripts;


    private void Start()
    {

        playerScripts = FindObjectsOfType<PickUpObject>();

        ReactionManager.Add("OrbPlaced", OrbPlaced);

        ReactionManager.Add("FirstOrb", FirstOrb);

        charIsClose = false;
        firstOrbPlaced = false;
        secondOrbPlaced = false;
        //HoverUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarriedOrb"))
        {
            charHasOrb = true;
        }

        if (other.CompareTag("Character")){

            charIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CarriedOrb"))
        {
            charHasOrb = false;
        }

        if (other.CompareTag("Character"))
        {
            charIsClose = false;
        }
    }

    private void Update()
    {
        if (charHasOrb == true && Input.GetKeyDown(KeyCode.E) && charIsClose == true)
        {
            ReactionManager.Call("OrbPlaced");
            charHasOrb = false;
        }
    }

    private void OrbPlaced(string[] empty)
    {
        if(firstOrbPlaced == true)
        {
            secondOrbPlaced = true;
            ReactionManager.Call("GameFinished");
            ReactionManager.Call("StartCutScene");

            gameObject.tag = "Untagged";
        }

        else if(firstOrbPlaced == false)
        {
            //firstOrbPlaced = true;

        }
    }

    private void FirstOrb(string[] empty)
    {
        for (var i = 0; i < playerScripts.Length; i++)
            playerScripts[i].firstOrbPlaced = true;

        firstOrbPlaced = true;
    }
}
