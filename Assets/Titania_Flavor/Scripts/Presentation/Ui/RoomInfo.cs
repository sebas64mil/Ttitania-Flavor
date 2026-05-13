using UnityEngine;
using Fusion;
using TMPro;

public class RoomInfo : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerCountText;

    [SerializeField]
    private TMP_Text roomCodeText;

    [SerializeField]
    private GameObject roomInfoPanel;

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
        if (roomInfoPanel != null)
        {
            roomInfoPanel.SetActive(true);
        }

        UpdatePlayerCount(default);
        UpdateRoomCode(roomCode);
    }

    private void UpdatePlayerCount(PlayerRef player)
    {
        if (playerCountText != null)
        {
            int count = LobbyNetwork.Instance.GetPlayerCount();
            playerCountText.text = $"Jugadores: {count}";
        }
    }

    private void UpdateRoomCode(string roomCode)
    {
        if (roomCodeText != null)
        {
            if (LobbyNetwork.Instance.IsHost())
            {
                roomCodeText.text = $"Código: {roomCode}";
                roomCodeText.gameObject.SetActive(true);
            }
            else
            {
                roomCodeText.gameObject.SetActive(false);
            }
        }
    }

    public void HideRoomInfo()
    {
        if (roomInfoPanel != null)
        {
            roomInfoPanel.SetActive(false);
        }
    }
}