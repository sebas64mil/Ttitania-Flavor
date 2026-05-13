using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    public string NameSceneLobby = "Lobby";

    public void HostGame()
    {
        LobbyNetwork.Instance.HostGame(NameSceneLobby);
    }

    public void JoinGame()
    {
        LobbyNetwork.Instance.JoinGame(NameSceneLobby);
    }

    public void Shutdown()
    {
        LobbyNetwork.Instance.Shutdown();
    }
}