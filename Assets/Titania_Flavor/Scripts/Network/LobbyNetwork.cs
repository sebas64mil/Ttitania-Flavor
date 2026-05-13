using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyNetwork : MonoBehaviour
{
    public static LobbyNetwork Instance;

    private NetworkRunner runner;

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

    public void HostGame(string sceneName)
    {
        StartGame(GameMode.Host, sceneName);
        GameManager.Instance.ChangeScene(sceneName);
    }

    // =========================
    // CLIENT
    // =========================

    public void JoinGame(string sceneName)
    {
        StartGame(GameMode.Client, sceneName);
        GameManager.Instance.ChangeScene(sceneName);
    }

    // =========================
    // START
    // =========================

    private async void StartGame(GameMode mode, string sceneName)
    {
        if (runner != null)
            return;

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

        Debug.Log($"Scene Path: Assets/Titania_Flavor/Scenes/{sceneName}.unity");
        Debug.Log($"Build Index: {buildIndex}");
        Debug.Log($"Scene Ref: {scene}");

        await runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,

            SessionName = "TestRoom",

            Scene = scene,

            SceneManager = sceneManager
        });
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