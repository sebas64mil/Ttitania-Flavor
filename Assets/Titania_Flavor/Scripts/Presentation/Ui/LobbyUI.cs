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

    public void HostGame()
    {
        string roomName = GetRoomName();
        LobbyNetwork.Instance.HostGame(NameSceneLobby, roomName);
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

    public void Shutdown()
    {
        LobbyNetwork.Instance.Shutdown();
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