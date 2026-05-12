using Fusion;
using UnityEngine;

public class LobbyPlayerInfo : MonoBehaviour
{
    public static LobbyPlayerInfo Instance;

    public int PlayerCount { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        LobbyEvents.OnPlayerJoined += PlayerJoined;
        LobbyEvents.OnPlayerLeft += PlayerLeft;
    }

    private void OnDisable()
    {
        LobbyEvents.OnPlayerJoined -= PlayerJoined;
        LobbyEvents.OnPlayerLeft -= PlayerLeft;
    }

    private void PlayerJoined(PlayerRef player)
    {
        PlayerCount++;

        LobbyEvents.OnPlayerCountChanged?.Invoke(PlayerCount);
    }

    private void PlayerLeft(PlayerRef player)
    {
        PlayerCount--;

        LobbyEvents.OnPlayerCountChanged?.Invoke(PlayerCount);
    }
}