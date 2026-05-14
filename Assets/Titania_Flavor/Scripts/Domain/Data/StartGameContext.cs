using Fusion;

public class StartGameContext
{
    public GameMode Mode;
    public string SceneName;
    public string RoomCode;
    public int MaxPlayers = 4;

    public NetworkRunner Runner;
    public SceneRef Scene;

    public INetworkSceneManager SceneManager;
}