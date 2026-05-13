using Fusion;

public class StartGameContext
{
    public GameMode Mode;
    public string SceneName;
    public string RoomCode;

    public NetworkRunner Runner;
    public SceneRef Scene;

    public INetworkSceneManager SceneManager;
}