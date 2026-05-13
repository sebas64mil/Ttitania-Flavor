using UnityEngine;
using Fusion;

public class RoomInfo : MonoBehaviour
{
    private LobbyUIReferences ui;

    private void Awake()
    {
        ui = LobbyUIReferences.Instance;
    }

    private void OnEnable()
    {
        LobbyEvents.OnRoomCreated += ShowRoomInfo;
        LobbyEvents.OnPlayerJoined += UpdatePlayerCount;
        LobbyEvents.OnPlayerLeft += UpdatePlayerCount;
    }

    private void OnDisable()
    {
        LobbyEvents.OnRoomCreated -= ShowRoomInfo;
        LobbyEvents.OnPlayerJoined -= UpdatePlayerCount;
        LobbyEvents.OnPlayerLeft -= UpdatePlayerCount;
    }

    private void ShowRoomInfo(string roomName, string roomCode)
    {
        if (!TryGetUIReferences())
            return;

        ui.RoomInfoPanel.SetActive(true);

        UpdatePlayerCount(default);
        UpdateRoomCode(roomCode);
    }

    private void UpdatePlayerCount(PlayerRef player)
    {
        if (!TryGetUIReferences())
            return;

        int count = LobbyNetwork.Instance.GetPlayerCount();

        ui.PlayerCountText.text =
            $"Jugadores: {count}";
    }

    private void UpdateRoomCode(string roomCode)
    {
        if (!TryGetUIReferences())
            return;

        if (LobbyNetwork.Instance.IsHost())
        {
            ui.RoomCodeText.text =
                $"Código: {roomCode}";

            ui.RoomCodeText.gameObject.SetActive(true);
        }
        else
        {
            ui.RoomCodeText.gameObject.SetActive(false);
        }
    }

    public void HideRoomInfo()
    {
        if (!TryGetUIReferences())
            return;

        ui.RoomInfoPanel.SetActive(false);
    }

    private bool TryGetUIReferences()
    {
        if (ui == null)
        {
            ui = LobbyUIReferences.Instance;
        }

        if (ui == null)
        {
            Debug.LogWarning(
                "[RoomInfo] LobbyUIReferences no está disponible aún");
            return false;
        }

        return true;
    }
}