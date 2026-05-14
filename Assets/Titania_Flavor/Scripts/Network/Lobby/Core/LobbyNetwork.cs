using Fusion;
using UnityEngine;
using System.Linq;

public class LobbyNetwork : MonoBehaviour
{
    public static LobbyNetwork Instance;

    private NetworkRunner runner;
    private string currentRoomCode;
    private string currentRoomName;
    private GameMode currentGameMode;
    private int maxPlayers = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        if (Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
    }

    public void HostGame(
        string sceneName,
        string roomName = "",
        int maxPlayersCount = 4)
    {
        maxPlayers = Mathf.Clamp(maxPlayersCount, 0, 4);
        currentRoomName = roomName;
        LobbyEvents.OnStartHosting?.Invoke();

        StartGameAsync(
            GameMode.Host,
            sceneName,
            "",
            maxPlayers);
    }

    public void JoinGame(
        string sceneName,
        string roomCode = "")
    {
        if (!CanJoinGame())
        {
            Debug.LogWarning(
                "[LobbyNetwork] No se puede unir: " +
                "ya hay un NetworkRunner activo");
            return;
        }

        LobbyEvents.OnStartJoining?.Invoke();

        StartGameAsync(
            GameMode.Client,
            sceneName,
            roomCode,
            4);
    }

    private bool CanJoinGame()
    {
        return runner == null || !runner;
    }

    private async void StartGameAsync(
        GameMode mode,
        string sceneName,
        string roomCode = "",
        int maxPlayersCount = 4)
    {
        if (runner != null)
        {
            if (runner.IsRunning)
            {
                Debug.LogWarning("Ya existe un NetworkRunner activo");
                return;
            }

            Destroy(runner);
            runner = null;

            await Awaitable.NextFrameAsync();
        }

        if (Instance == null || Instance.gameObject == null)
        {
            Debug.LogError("[LobbyNetwork] LobbyNetwork.Instance ha sido destruido");
            return;
        }

        currentGameMode = mode;
        maxPlayers = Mathf.Clamp(maxPlayersCount, 0, 4);
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        var context = new StartGameContext
        {
            Mode = mode,
            SceneName = sceneName,
            RoomCode = roomCode,
            MaxPlayers = maxPlayers,
            Runner = runner,
            SceneManager = null
        };

        ILobbyStartHandler handler = new FusionStartHandler();
        handler = new CallbackDecorator(handler);
        handler = new SceneDecorator(handler);
        handler = new RoomCodeDecorator(handler);

        try
        {
            await handler.StartGame(context);
            currentRoomCode = context.RoomCode;
            LobbyEvents.OnFinishedLoading?.Invoke();
            Debug.Log("[LobbyNetwork] Conexión completada");
        }
        catch (System.Exception ex)
        {
            LobbyEvents.OnFinishedLoading?.Invoke();
            Debug.LogError($"[LobbyNetwork] Error al iniciar juego: {ex.Message}\n{ex.StackTrace}");

            if (runner != null)
            {
                Destroy(runner);
                runner = null;
            }

            ResetLobbyState();
        }
    }

    public void ResetLobbyState()
    {
        if (runner != null)
        {
            Destroy(runner);
        }

        runner = null;

        currentRoomCode = string.Empty;
        currentRoomName = string.Empty;
        currentGameMode = 0;
        maxPlayers = 4;

        GameManager.Instance.ChangeScene("Menu");
    }

    public int GetPlayerCount()
    {
        if (runner != null && runner.IsRunning)
        {
            return runner.ActivePlayers.Count();
        }

        return 0;
    }

    public int GetMaxPlayers()
    {
        return maxPlayers;
    }

    public string GetRoomCode()
    {
        return currentRoomCode;
    }

    public string GetRoomName()
    {
        return currentRoomName;
    }

    public string GetSessionName()
    {
        if (runner != null)
        {
            return runner.SessionInfo.Name;
        }

        return string.Empty;
    }

    public bool IsHost()
    {
        if (runner != null && runner.IsRunning)
        {
            return runner.IsServer;
        }

        return currentGameMode == GameMode.Host;
    }

    public bool IsClient()
    {
        if (runner != null && runner.IsRunning)
        {
            return runner.IsClient;
        }

        return currentGameMode == GameMode.Client;
    }

    public async void Shutdown()
    {
        if (runner == null)
        {
            return;
        }

        await runner.Shutdown();
        ResetLobbyState();
        Debug.Log("[LobbyNetwork] Sesión finalizada");
    }

    public NetworkRunner GetRunner()
    {
        return runner;
    }
}
