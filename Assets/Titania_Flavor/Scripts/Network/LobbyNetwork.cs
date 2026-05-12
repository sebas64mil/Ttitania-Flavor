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


    public void HostGame()
    {
        StartGame(GameMode.Host);
    }

    public void JoinGame()
    {
        StartGame(GameMode.Client);
    }


    private async void StartGame(GameMode mode)
    {
        if (runner != null)
            return;


        runner = gameObject.AddComponent<NetworkRunner>();

        runner.ProvideInput = true;


        if (FusionCallbacks.Instance == null)
        {
            gameObject.AddComponent<FusionCallbacks>();
        }


        if (GetComponent<NetworkSceneManagerDefault>() == null)
        {
            gameObject.AddComponent<NetworkSceneManagerDefault>();
        }



        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);



        await runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,

            SessionName = "TestRoom",

            Scene = scene,

            SceneManager = GetComponent<NetworkSceneManagerDefault>()
        });
    }


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