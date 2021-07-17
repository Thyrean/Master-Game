using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public PlayerPanelManager manager;
    [SerializeField] private Text text;

    public NetworkConnection connection;
    public PlayerId playerId;

    public void SetConnection(NetworkConnection connection)
    {
        this.connection = connection;
        var obj = connection.identity?.gameObject;
        var playerId = obj?.GetComponent<PlayerId>();
        if (playerId == null)
        {
            Debug.Log($"<color=red>Player {connection} has no object !</color>");
            return;
        }

        this.playerId = playerId;
    }

    public void ResetConnection()
    {
        playerId = null;
        connection = null;
        text.text = "<color=red>Not connected !</color>";
    }

    public void OnClick()
    {
        manager.Toggle();
    }

    public void SetPlayer(bool isPlayerA)
    {
        if(connection == null) return;

        playerId.SetPlayerA(isPlayerA);
        text.text = $"<color=green>{connection.address} = {(isPlayerA ? "A" : "B")}</color>";
    }
}
