using System;
using Assets.Scripts.ActionReactionSystem;
using Mirror;

namespace Helpers
{
    public class CustomNetworkManager : NetworkManager
    {
        public static Action OnConnect;
        public static Action OnServerStart;
        public static Action<NetworkConnection> OnServerAddedPlayer;
        public static Action<NetworkConnection> OnClientDisconnected; 

        public override void Awake()
        {
            base.Awake();
            ReactionManager.BindToNetwork();
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            OnConnect?.Invoke();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            OnServerStart?.Invoke();
        }

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            base.OnServerAddPlayer(conn);
            OnServerAddedPlayer?.Invoke(conn);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            OnClientDisconnected?.Invoke(conn);
        }
    }
}