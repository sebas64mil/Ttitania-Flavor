using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class FusionCallbacks : MonoBehaviour, INetworkRunnerCallbacks
{
    public static FusionCallbacks Instance;
    private bool isShuttingDown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            isShuttingDown = false;
            return;
        }

        Destroy(gameObject);
    }

    void INetworkRunnerCallbacks.OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[FusionCallbacks] Player Joined: {player}");

        LobbyEvents.OnPlayerJoined?.Invoke(player);

        if (!isShuttingDown)
        {
            LobbyEvents.OnFinishedLoading?.Invoke();
        }
    }

    void INetworkRunnerCallbacks.OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[FusionCallbacks] Player Left: {player}");

        LobbyEvents.OnPlayerLeft?.Invoke(player);
    }

    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("[FusionCallbacks] Connected to Server");

        if (LobbyNetwork.Instance.IsHost())
        {
            LobbyEvents.OnStartHosting?.Invoke();
        }
        else if (runner.GameMode == GameMode.Client)
        {
            LobbyEvents.OnStartJoining?.Invoke();
        }
    }

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.Log($"[FusionCallbacks] Disconnected from Server: {reason}");

        isShuttingDown = true;

        LobbyEvents.OnDisconnected?.Invoke();

        if (LobbyNetwork.Instance.IsClient())
        {
            Debug.Log("[FusionCallbacks] Cliente normal desconectándose");
            LobbyNetwork.Instance.ResetLobbyState();
        }

        isShuttingDown = false;
    }

    void INetworkRunnerCallbacks.OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        Debug.Log("[FusionCallbacks] Solicitud de conexión recibida");

        if (LobbyNetwork.Instance.IsHost())
        {
            int currentPlayerCount = runner.ActivePlayers.Count();
            int maxPlayers = LobbyNetwork.Instance.GetMaxPlayers();

            if (currentPlayerCount >= maxPlayers)
            {
                request.Refuse();
                return;
            }

            request.Accept();
        }
    }

    void INetworkRunnerCallbacks.OnInput(NetworkRunner runner, NetworkInput input) { }

    void INetworkRunnerCallbacks.OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    void INetworkRunnerCallbacks.OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log($"[FusionCallbacks] NetworkRunner Shutdown. Reason: {shutdownReason}");

        isShuttingDown = true;

        if (LobbyNetwork.Instance.IsClient())
        {
            Debug.Log("[FusionCallbacks] Cliente desconectado por OnShutdown");
            LobbyNetwork.Instance.ResetLobbyState();
        }

        isShuttingDown = false;
    }

    void INetworkRunnerCallbacks.OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log($"[FusionCallbacks] Connect Failed: {reason}");

        if (LobbyNetwork.Instance.IsClient())
        {
            isShuttingDown = true;
            LobbyNetwork.Instance.ResetLobbyState();
            isShuttingDown = false;
        }
    }

    void INetworkRunnerCallbacks.OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

    void INetworkRunnerCallbacks.OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

    void INetworkRunnerCallbacks.OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    void INetworkRunnerCallbacks.OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        Debug.Log("[FusionCallbacks] Host Migration detectado - ignorado");
    }

    void INetworkRunnerCallbacks.OnSceneLoadDone(NetworkRunner runner) 
    {
        Debug.Log("[FusionCallbacks] Scene loaded");

        if (!isShuttingDown)
        {
            LobbyEvents.OnFinishedLoading?.Invoke();
        }
    }

    void INetworkRunnerCallbacks.OnSceneLoadStart(NetworkRunner runner) { }

    void INetworkRunnerCallbacks.OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    void INetworkRunnerCallbacks.OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    void INetworkRunnerCallbacks.OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

    void INetworkRunnerCallbacks.OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
}