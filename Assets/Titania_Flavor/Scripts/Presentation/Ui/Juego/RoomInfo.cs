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

        string nameToShow = roomName;

        if (string.IsNullOrEmpty(nameToShow) && LobbyNetwork.Instance != null)
        {
            nameToShow = LobbyNetwork.Instance.GetRoomName();
        }

        if (string.IsNullOrEmpty(nameToShow) && LobbyNetwork.Instance != null)
        {
            nameToShow = LobbyNetwork.Instance.GetSessionName();
        }

        if (string.IsNullOrEmpty(nameToShow))
        {
            nameToShow = roomCode;
        }

        UpdateRoomName(nameToShow);
        UpdateRoomCode(roomCode);
    }

    private void UpdatePlayerCount(PlayerRef player)
    {
        if (!TryGetUIReferences())
            return;

        int count = LobbyNetwork.Instance.GetPlayerCount();
        int maxPlayers = LobbyNetwork.Instance.GetMaxPlayers();

        ui.PlayerCountText.text =
            $"{count}/{maxPlayers}";
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

    private void UpdateRoomName(string roomName)
    {
        if (!TryGetUIReferences())
            return;

        if (!LobbyNetwork.Instance.IsHost())
        {

            ui.RoomNameText.gameObject.SetActive(true);
            return;
        }
        else
        {
            ui.RoomNameText.gameObject.SetActive(false);
        }

        if (string.IsNullOrEmpty(roomName))
        {
            ui.RoomNameText.gameObject.SetActive(false);
            ui.RoomNameText.text = string.Empty;
            return;
        }

        ui.RoomNameText.text = $"Sala: {roomName}";
        ui.RoomNameText.gameObject.SetActive(true);
    }

    public void HideRoomInfo()
    {
        if (!TryGetUIReferences())
            return;

        ui.RoomInfoPanel.SetActive(false);

        if (ui.RoomNameText != null)
        {
            ui.RoomNameText.gameObject.SetActive(false);
            ui.RoomNameText.text = string.Empty;
        }

        if (ui.RoomCodeText != null)
        {
            ui.RoomCodeText.gameObject.SetActive(false);
            ui.RoomCodeText.text = string.Empty;
        }
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