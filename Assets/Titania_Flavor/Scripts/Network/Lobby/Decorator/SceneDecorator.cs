using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class SceneDecorator : LobbyDecorator
{
    public SceneDecorator(ILobbyStartHandler next)
        : base(next)
    {
    }

    public override async Task StartGame(StartGameContext context)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(
            $"Assets/Titania_Flavor/Scenes/{context.SceneName}.unity");
        
        context.Scene = SceneRef.FromIndex(buildIndex);

        GameManager.Instance.ChangeScene(context.SceneName);

        await base.StartGame(context);
    }
}