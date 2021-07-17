using System.Collections;
using System.Collections.Generic;
using Helpers;
using Mirror;
using Telepathy;
using UnityEngine;

public class PlayerPanelManager : MonoBehaviour
{
    public PlayerPanel aPanel;
    public PlayerPanel bPanel;

    private void Awake()
    {
        gameObject.SetActive(false);
        CustomNetworkManager.OnServerStart += () => gameObject.SetActive(true);
        CustomNetworkManager.OnServerAddedPlayer += AddPlayer;
        CustomNetworkManager.OnClientDisconnected += RemovePlayer;
        
    }

    private void RemovePlayer(NetworkConnection connection)
    {
        if(aPanel.connection == connection) aPanel.ResetConnection();
        else if(bPanel.connection == connection) bPanel.ResetConnection();
    }

    private void AddPlayer(NetworkConnection connection)
    {
        if (aPanel.connection == null)
        {
            aPanel.SetConnection(connection);
            Toggle();
        }
        else if (bPanel.connection == null)
        {
            bPanel.SetConnection(connection);
            Toggle();
        }
    }

    public void Toggle()
    {
        var player = aPanel?.playerId ?? bPanel?.playerId;
        if (player == null) return;
        aPanel.SetPlayer(!player.isPlayerA);
        bPanel.SetPlayer(!player.isPlayerA);
    }
}
