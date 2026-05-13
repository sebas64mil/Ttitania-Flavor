using Fusion;
using UnityEngine;
using System.Linq;

public class LobbyNetwork : MonoBehaviour
{
    public static LobbyNetwork Instance;

    private NetworkRunner runner;
    private string currentRoomCode;
    private GameMode currentGameMode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public void HostGame(string sceneName, string roomName = "")
    {
        StartGameAsync(GameMode.Host, sceneName, "");
    }



    public void JoinGame(string sceneName, string roomCode = "")
    {
        StartGameAsync(GameMode.Client, sceneName, roomCode);
    }
    private async void StartGameAsync(GameMode mode, string sceneName, string roomCode = "")
    {
        if (runner != null)
        {
            Debug.LogWarning("Ya existe un NetworkRunner activo");
            return;
        }

        currentGameMode = mode;
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        var context = new StartGameContext
        {
            Mode = mode,
            SceneName = sceneName,
            RoomCode = roomCode,
            Runner = runner,
            SceneManager = null
        };

        ILobbyStartHandler handler = new FusionStartHandler();
        handler = new CallbackDecorator(handler);
        handler = new SceneDecorator(handler);
        handler = new RoomCodeDecorator(handler);
        handler = new LoggingDecorator(handler);

        try
        {
            await handler.StartGame(context);

            currentRoomCode = context.RoomCode;
            Debug.Log("Conexión completada");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[LobbyNetwork] Error al iniciar juego: {ex.Message}\n{ex.StackTrace}");

            if (runner != null)
            {
                Destroy(runner);
                runner = null;
            }
        }
    }

    public int GetPlayerCount()
    {
        if (runner != null && runner.IsRunning)
        {
            return runner.ActivePlayers.Count();
        }

        return 0;
    }

    public string GetRoomCode()
    {
        return currentRoomCode;
    }

    public string GetSessionName()
    {
        if (runner != null)
        {
            return runner.SessionInfo.Name;
        }

        return string.Empty;
    }

    public GameMode GetGameMode()
    {
        return currentGameMode;
    }

    public bool IsHost()
    {
        return currentGameMode == GameMode.Host;
    }

    public bool IsClient()
    {
        return currentGameMode == GameMode.Client;
    }

    public async void Shutdown()
    {
        if (runner == null)
            return;

        await runner.Shutdown();

        runner = null;
        currentRoomCode = string.Empty;

        Debug.Log("[LobbyNetwork] Sesión finalizada");
    }

    public NetworkRunner GetRunner()
    {
        return runner;
    }

    public bool HasRunner()
    {
        return runner != null;
    }
}