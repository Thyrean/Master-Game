using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using UnityEngine;
using Mirror;

public class TeleporterPads : NetworkBehaviour 
{

    public bool lowerPadActive;
    public bool upperPadActive;

    [SyncVar]
    public bool justTeleported;

    void Start()
    {
        justTeleported = false;

        ReactionManager.Add("activateUpperPad", ActivateUpperPad);
        ReactionManager.Add("activateLowerPad", ActivateLowerPad);
        ReactionManager.Add("deactivateUpperPad", DeactivateUpperPad);
        ReactionManager.Add("deactivateLowerPad", DeactivateLowerPad);
    }

    private void Update()
    {
        if (justTeleported == false)
        {
            if (lowerPadActive == true && upperPadActive == true)
            {
                //Debug.Log("Both active");
                ReactionManager.Call("TeleportPlayers");
                justTeleported = true;
            }
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
}
