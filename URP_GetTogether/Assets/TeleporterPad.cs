using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;

using UnityEngine;
using Mirror;

public class TeleporterPad : NetworkBehaviour
{
    /*[SyncVar]
    public bool playerOnTop;

    public GameObject playerToTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "upperPad")
            ReactionManager.Call("ActivateUpperPad");
        if (gameObject.tag == "lowerPad")
            ReactionManager.Call("ActivateLowerPad");

        //playerOnTop = true;
        //playerToTeleport = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        playerOnTop = false;

        playerToTeleport = null;
    }*/
}
