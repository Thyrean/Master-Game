using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using Lightbug.CharacterControllerPro.Core;

using UnityEngine;
using Mirror;

public class TeleporterPad : MonoBehaviour
{
    public GameObject otherPad;
    public GameObject currentPlayer;

    void Start()
    {

        ReactionManager.Add("TeleportPlayers", TeleportPlayers);
        //savedPos = otherPad.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "upperPad" && other.tag == "Character")
        {
            ReactionManager.Call("activateUpperPad");
            currentPlayer = other.gameObject;
        }
        if (gameObject.tag == "lowerPad" && other.tag == "Character")
        {
            ReactionManager.Call("activateLowerPad");
            currentPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag == "upperPad")
        {
            ReactionManager.Call("deactivateUpperPad");
            currentPlayer = null;
        }
        if (gameObject.tag == "lowerPad")
        {
            ReactionManager.Call("deactivateLowerPad");
            currentPlayer = null;
        }
    }
    private void TeleportPlayers(string[] empty)
    {
        if (currentPlayer != null)
        {
            if (gameObject.tag == "upperPad")
            {
                Debug.Log("Teleporting upper player");
                currentPlayer.GetComponent<CharacterActor>().Teleport(otherPad.transform.position, otherPad.transform.rotation);
                currentPlayer = null;
            }
            if (gameObject.tag == "lowerPad")
            {
                Debug.Log("Teleporting lower player");
                currentPlayer.GetComponent<CharacterActor>().Teleport(otherPad.transform.position, otherPad.transform.rotation);
                currentPlayer = null;
            }
        }
        else return;
    }
}
