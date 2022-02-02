using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NetworkedInputField : MonoBehaviour
{
    [SerializeField] private NetworkedInputElement[] elements;

    private void Start()
    {
        foreach (var element in elements)
            element.OnChanged += Changed;


    }

    private void Changed(NetworkedInputElement obj)
    {
        Debug.Log($"TEMP : {obj.name} changed.");
        for(var i=0; i<elements.Length; i++)
        {
            if (obj != elements[i])
            {
                continue;
            }

            var message = new InputElementMessage(i, obj);
            Send(message);
            return;
        }
        Debug.Log("No matching element found!");
    }

    private void Send(InputElementMessage message)
    {
        if (NetworkServer.active)
        {
            Debug.Log($"TEMP : {message.index} sending to clients.");
            ForwardMessageToClients(null, message);
        }
        else
        {
            Debug.Log($"TEMP : {message.index} sending to server.");
            NetworkClient.Send(message);
        }
    }

    private void BindHandlers()
    {
        if (NetworkServer.active)
        {
            NetworkServer.RegisterHandler<InputElementMessage>((c, m) => { ForwardMessageToClients(c, m); Receive(c, m); }) ;

            Debug.Log($"TEMP : server forward registered.");
        }
        else
        {

            NetworkClient.RegisterHandler<InputElementMessage>(Receive);
            Debug.Log($"TEMP : client receive registered.");
        }
    }

    private void Receive(NetworkConnection connection, InputElementMessage message)
    {
        Debug.Log($"TEMP : {message.text} received.");
        var index = message.index;
        message.SetValues(elements[index]);
    }

    private void ForwardMessageToClients(NetworkConnection connection, InputElementMessage message)
    {
        foreach (var client in NetworkServer.connections)
        {
            Debug.Log($"TEMP : {message.text} sent to {client.Value.address}.");
            client.Value.Send(message);
        }
    }
}

public class InputElementMessage : NetworkMessage
{
    public Color color;
    public string text;
    public int index;

    public InputElementMessage()
    {

    }

    public InputElementMessage(int index, NetworkedInputElement element)
    {
        this.index = index;
        color = element.Color;
        text = element.Text;
    }

    public void SetValues(NetworkedInputElement element)
    {
        element.Color = color;
        element.Text = text;
    }
}
