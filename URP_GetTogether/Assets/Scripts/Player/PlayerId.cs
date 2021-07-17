using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerId : NetworkBehaviour
{
    [SyncVar] public bool isPlayerA;

    public static PlayerId LocalPlayer{
        get;
        private set;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        LocalPlayer = this;
    }
    private void OnDestroy()
    {
        if (LocalPlayer == this)
            LocalPlayer = null;
    }

    //[Command]
    public void SetPlayerA(bool isPlayerA)
    {
        this.isPlayerA = isPlayerA;
    }

    public static bool IsPlayerA(NetworkConnection connection)
    {
        var obj = connection?.identity?.gameObject;
        var playerId = obj?.GetComponent<PlayerId>();
        if (playerId == null)
        {
            Debug.Log("Network Connection has no valid player object");
            return false;
        }

        return playerId.isPlayerA;
    }
}
