using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public class FusionStartHandler : ILobbyStartHandler
{
    public async Task StartGame(StartGameContext context)
    {
        if (context.Runner.GetComponent<FusionCallbacks>() == null)
        {
            context.Runner.gameObject.AddComponent<FusionCallbacks>();
        }

        if (context.SceneManager == null)
        {
            context.SceneManager = context.Runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        await context.Runner.StartGame(new StartGameArgs()
        {
            GameMode = context.Mode,
            SessionName = context.RoomCode,
            Scene = context.Scene,
            SceneManager = context.SceneManager
        });
    }
}