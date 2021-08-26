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

    private void Start()
    {
        ReactionManager.Add("OrbPlaced", OrbPlaced);

        charIsClose = false;
        firstOrbPlaced = false;
        secondOrbPlaced = false;
        //HoverUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            charHasOrb = true;
        }

        if (other.CompareTag("Character")){

            gameObject.tag = "InsertOrb";
            charIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            charHasOrb = false;
        }

        if (other.CompareTag("Character"))
        {

            gameObject.tag = "orbConsole";
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
        }

        else if(firstOrbPlaced == false)
        {
            firstOrbPlaced = true;
        }
    }
}
