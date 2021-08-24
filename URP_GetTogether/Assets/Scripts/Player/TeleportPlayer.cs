using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ActionReactionSystem;
using Lightbug.CharacterControllerPro.Core;
using Mirror;

public class TeleportPlayer : NetworkBehaviour
{
    private void Start()
    {
        Debug.Log("Teleport script feedback");

        if (!NetworkServer.active)
        {
            Debug.Log("Player 1 teleported to 0, 75, 0");

            gameObject.GetComponent<CharacterActor>().Teleport(new Vector3(0f,62f,-15f));
        }
        if (NetworkServer.active)
        {
            Debug.Log("Player 1 teleported to , 0, 0");
            gameObject.GetComponent<CharacterActor>().Teleport(new Vector3(0f, 0f, -15f));
        }
        //gameObject.GetComponent<TeleportPlayer>().enabled = false;

    }
}



