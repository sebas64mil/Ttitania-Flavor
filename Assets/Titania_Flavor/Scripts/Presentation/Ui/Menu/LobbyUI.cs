using UnityEngine;
using Fusion;
using TMPro;

public class LobbyUI : MonoBehaviour
{
    public string NameSceneLobby = "Lobby";

    [SerializeField]
    private TMP_InputField roomNameInputField;

    [SerializeField]
    private TMP_InputField roomCodeInputField;

    [SerializeField]
    private TMP_Text playerCountDisplay;

    private int currentMaxPlayers = 4;
    private const int MIN_PLAYERS = 2;
    private const int MAX_PLAYERS = 4;

    private void Start()
    {
        if (playerCountDisplay != null)
        {
            UpdatePlayerCountDisplay();
        }
    }

    public void HostGame()
    {
        string roomName = GetRoomName();
        LobbyNetwork.Instance.HostGame(NameSceneLobby, roomName, currentMaxPlayers);
    }

    public void JoinGame()
    {
        string roomCode = GetRoomCode();

        if (string.IsNullOrWhiteSpace(roomCode))
        {
            Debug.LogWarning("El código de sala no puede estar vacío");
            return;
        }

        if (!IsValidRoomCode(roomCode))
        {
            Debug.LogWarning($"Código de sala inválido: {roomCode}. Debe contener solo números (4 dígitos)");
            return;
        }

        LobbyNetwork.Instance.JoinGame(NameSceneLobby, roomCode);
    }

    public void IncreasePlayerCount()
    {
        if (currentMaxPlayers < MAX_PLAYERS)
        {
            currentMaxPlayers++;
            UpdatePlayerCountDisplay();
            Debug.Log($"Máximo de jugadores aumentado a: {currentMaxPlayers}");
        }
        else
        {
            Debug.LogWarning($"Ya se alcanzó el máximo de {MAX_PLAYERS} jugadores");
        }
    }

    public void DecreasePlayerCount()
    {
        if (currentMaxPlayers > MIN_PLAYERS)
        {
            currentMaxPlayers--;
            UpdatePlayerCountDisplay();
            Debug.Log($"Máximo de jugadores disminuido a: {currentMaxPlayers}");
        }
        else
        {
            Debug.LogWarning($"Ya se alcanzó el mínimo de {MIN_PLAYERS} jugadores");
        }
    }

    public int GetMaxPlayerCount()
    {
        return currentMaxPlayers;
    }

    public void SetMaxPlayerCount(int count)
    {
        if (count >= MIN_PLAYERS && count <= MAX_PLAYERS)
        {
            currentMaxPlayers = count;
            UpdatePlayerCountDisplay();
        }
        else
        {
            Debug.LogWarning($"El número de jugadores debe estar entre {MIN_PLAYERS} y {MAX_PLAYERS}");
        }
    }

    private void UpdatePlayerCountDisplay()
    {
        if (playerCountDisplay != null)
        {
            playerCountDisplay.text = $"{currentMaxPlayers}";
        }
    }

    public string GetRoomName()
    {
        if (roomNameInputField != null)
            return roomNameInputField.text;

        return string.Empty;
    }

    public string GetRoomCode()
    {
        if (roomCodeInputField != null)
            return roomCodeInputField.text;

        return string.Empty;
    }

    private bool IsValidRoomCode(string roomCode)
    {
        return !string.IsNullOrWhiteSpace(roomCode) && 
               roomCode.Length == 4 && 
               int.TryParse(roomCode, out _);
    }
}