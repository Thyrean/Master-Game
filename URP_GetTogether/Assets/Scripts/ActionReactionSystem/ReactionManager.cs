using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Helpers;
using Mirror;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.ActionReactionSystem
{
    public class ReactionManager : Singleton<ReactionManager>
    {
        private Dictionary<string, ActionEventHandler> actions = new Dictionary<string, ActionEventHandler>();

        public static void BindToNetwork()
        {
            CustomNetworkManager.OnConnect += () => NetworkClient.RegisterHandler<ActionMessage>(Instance.OnActionReceivedClient);
            CustomNetworkManager.OnServerStart += () => NetworkServer.RegisterHandler<ActionMessage>(Instance.ForwardMessageToClients);
        }

        private void OnActionReceivedClient(NetworkConnection connection, ActionMessage message)
        {
            var actionName = message.actionName;
            if (!actions.ContainsKey(actionName))
            {
                Debug.Log($"<color=red>Action with name {actionName} recieved but not registered !<color>");
                return;
            }

            actions[actionName].Invoke(message.parameters);
        }

        private void ForwardMessageToClients(NetworkConnection connection, ActionMessage message)
        {
            foreach (var client in NetworkServer.connections)
            {
                client.Value.Send(message);
            }
        }

        public static void Call(string actionName, params string[] parameters)
        {
            if (NetworkServer.active)
                Instance.ForwardMessageToClients(null, new ActionMessage(actionName, parameters));
            else
                NetworkClient.Send(new ActionMessage(actionName, parameters));
        }

        public static void Add(string actionName, Action<string[]> action)
        {
            if (!Instance.actions.ContainsKey(actionName))
            {
                Instance.actions.Add(actionName, new ActionEventHandler(actionName));
            }

            Instance.actions[actionName].actions += action;
        }
        public static void Remove(string actionName, Action<string[]> action)
        {
            if (Instance.actions.ContainsKey(actionName))
            {
                Instance.actions[actionName].actions -= action;
            }
        }
    }

    public class ActionMessage : NetworkMessage
    {
        public string actionName;
        public string[] parameters;

        public ActionMessage()
        {
            parameters = new string[0];
        }

        public ActionMessage(string actionName, params string[] parameters)
        {
            this.actionName = actionName;
            this.parameters = parameters??new string[0]; // if parameters = 0, weise parameters leeres Array zu
        }
    }

    internal class ActionEventHandler
    {
        public string actionName;

        public ActionEventHandler(string actionName)
        {
            this.actionName = actionName;
        }

        public event Action<string[]> actions;


        public void Invoke(string[] parameters)
        {
            actions?.Invoke(parameters);
        }
    }
}