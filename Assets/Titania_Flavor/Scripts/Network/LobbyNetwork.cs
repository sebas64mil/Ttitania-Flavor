using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    // =========================
    // HOST
    // =========================

    public void HostGame(string sceneName, string roomName = "")
    {
        StartGame(GameMode.Host, sceneName, roomName, "");
        GameManager.Instance.ChangeScene(sceneName);
    }

    // =========================
    // CLIENT
    // =========================

    public void JoinGame(string sceneName, string roomCode = "")
    {
        if (string.IsNullOrEmpty(roomCode))
        {
            Debug.LogError("No se puede unir sin un código de sala válido");
            return;
        }

        StartGame(GameMode.Client, sceneName, "", roomCode);
        GameManager.Instance.ChangeScene(sceneName);
    }

    // =========================
    // START
    // =========================

    private async void StartGame(GameMode mode, string sceneName, string roomName = "", string roomCode = "")
    {
        if (runner != null)
        {
            Debug.LogWarning("Ya existe un NetworkRunner activo");
            return;
        }

        currentGameMode = mode;
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = true;

        // CALLBACKS
        if (GetComponent<FusionCallbacks>() == null)
        {
            gameObject.AddComponent<FusionCallbacks>();
        }

        // SCENE MANAGER
        NetworkSceneManagerDefault sceneManager =
            gameObject.AddComponent<NetworkSceneManagerDefault>();

        // OBTENER SCENE REF
        int buildIndex = SceneUtility.GetBuildIndexByScenePath($"Assets/Titania_Flavor/Scenes/{sceneName}.unity");
        SceneRef scene = SceneRef.FromIndex(buildIndex);


        string sessionName;
        
        if (mode == GameMode.Host)
        {
            // El host genera un nuevo código de sala
            currentRoomCode = GenerateRoomCode();
            sessionName = currentRoomCode;
            
            Debug.Log($"[LobbyNetwork] HOST creado con código: {currentRoomCode}");
        }
        else
        {
            // El cliente se une usando el código proporcionado
            currentRoomCode = roomCode;
            sessionName = roomCode;
            
            Debug.Log($"[LobbyNetwork] CLIENT intentando unirse a sala: {roomCode}");
        }


        Debug.Log($"Iniciando juego en modo {mode} con sesión '{sessionName}' y código de sala '{currentRoomCode}'");

        try
        {
            await runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionName,
                Scene = scene,
                SceneManager = sceneManager
            });

            Debug.Log($"[LobbyNetwork] Conexión exitosa en modo {mode}");

            if (mode == GameMode.Host)
            {
                LobbyEvents.OnRoomCreated?.Invoke(sessionName, currentRoomCode);
            }
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

    private string GenerateRoomCode()
    {
        return Random.Range(1000, 9999).ToString();
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

    // =========================
    // SHUTDOWN
    // =========================

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