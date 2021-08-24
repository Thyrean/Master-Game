using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;

using UnityEngine;

public class orbPlacemen : MonoBehaviour
{
    public bool charHasOrb = false;

    public bool firstOrbPlaced;
    public bool secondOrbPlaced;

    private void Start()
    {
        ReactionManager.Add("OrbPlaced", OrbPlaced);
     
        firstOrbPlaced = false;
        secondOrbPlaced = false;
        //HoverUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            charHasOrb = true;
            gameObject.tag = "InsertOrb";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            charHasOrb = false;
            gameObject.tag = "Untagged";
        }
    }

    private void Update()
    {
        if (charHasOrb == true && Input.GetKeyDown(KeyCode.E))
        {
            ReactionManager.Call("OrbPlaced");
        }
    }

    private void OrbPlaced(string[] empty)
    {
        if(firstOrbPlaced == true)
        {
            secondOrbPlaced = true;
            ReactionManager.Call("GameFinish");
        }

        else if(firstOrbPlaced == false)
        {
            firstOrbPlaced = true;
        }
    }
}
