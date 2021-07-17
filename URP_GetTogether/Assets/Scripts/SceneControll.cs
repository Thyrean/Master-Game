using System.Collections;
using System.Collections.Generic;
using Helpers;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMessage : NetworkMessage
{
    public int sceneIndex;
}

public class SceneControll : MonoBehaviour
{
    private void Awake()
    {
        CustomNetworkManager.OnConnect += () => NetworkClient.RegisterHandler<LoadSceneMessage>(message => SceneManager.LoadScene(message.sceneIndex));
        CustomNetworkManager.OnServerStart += () => gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void LoadForBoth()
    { 
        LoadForBoth(1,1);
    }

    public void LoadForBoth(int aSceneIndex, int bSceneIndex)
    {
        foreach (var connection in NetworkServer.connections)
        {
            var isPlayerA = PlayerId.IsPlayerA(connection.Value);
            var loadMessage = new LoadSceneMessage();
            loadMessage.sceneIndex = isPlayerA ? aSceneIndex : bSceneIndex;
            connection.Value.Send(loadMessage);
        }
    }
}
