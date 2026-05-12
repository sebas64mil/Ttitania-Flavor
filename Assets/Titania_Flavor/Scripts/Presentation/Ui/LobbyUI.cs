using UnityEngine;

public class LobbyUI : MonoBehaviour
{


    public void HostGame()
    {
        LobbyNetwork.Instance.HostGame();
    }


    public void JoinGame()
    {
        LobbyNetwork.Instance.JoinGame();
    }

    public void Shutdown()
    {
        LobbyNetwork.Instance.Shutdown();
    }
}
