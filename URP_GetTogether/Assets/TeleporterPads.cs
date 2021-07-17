using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using UnityEngine;
using Mirror;

public class TeleporterPads : MonoBehaviour
{
    //public GameObject lowerPad;
    //public GameObject upperPad;

    public GameObject upperPlayer;
    public GameObject lowerPlayer;

    public bool lowerPadActive;
    public bool upperPadActive;

    public bool bothActive;

 
    void Start()
    {
        ReactionManager.Add("activateUpperPad", ActivateUpperPad);
        ReactionManager.Add("activateLowerPad", ActivateLowerPad);
        ReactionManager.Add("deactivateUpperPad", DeactivateUpperPad);
        ReactionManager.Add("deactivateLowerPad", DeactivateLowerPad);

        ReactionManager.Add("Teleport", TeleportPlayers);
    }

    private void Update()
    {
        if(lowerPadActive == true && upperPadActive == true)
        {
            bothActive = true;
        
        }

        if(Input.GetKeyDown(KeyCode.T) && bothActive == true)
        {
            ReactionManager.Call("Teleport");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "upperPad")
        {
            ReactionManager.Call("activateUpperPad");
            upperPlayer = other.gameObject;
        }
        if (gameObject.tag == "lowerPad")
        {
            ReactionManager.Call("activateLowerPad");
            lowerPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "upperPad")
        {
            ReactionManager.Call("deactivateUpperPad");
            upperPlayer = null;
        }
        if (gameObject.tag == "lowerPad")
        { 
            ReactionManager.Call("deactivateLowerPad");
            lowerPlayer = null;
        }
    }

    private void ActivateUpperPad(string[] empty)
    {
        upperPadActive = true;
    }

    private void ActivateLowerPad(string[] empty)
    {
        lowerPadActive = true;
    }

    private void DeactivateUpperPad(string[] empty)
    {
        upperPadActive = false;
    }

    private void DeactivateLowerPad(string[] empty)
    {
        lowerPadActive = false;
    }

    private void TeleportPlayers(string[] empty)
    {
        Debug.Log("Teleport called!");

        upperPlayer.transform.position = new Vector3(0, 0, 0);

        lowerPlayer.transform.position = new Vector3(0, 70, 0);
    }
}
