using Fusion;
using Fusion.Sockets;
using System;

public static class LobbyEvents
{
    public static Action<PlayerRef> OnPlayerJoined;

    public static Action<PlayerRef> OnPlayerLeft;

    public static Action OnStartHosting;

    public static Action OnStartJoining;

    public static Action OnFinishedLoading;

    public static Action<int> OnPlayerCountChanged;

    public static Action<string, string> OnRoomCreated;

    public static Action OnDisconnected;
}