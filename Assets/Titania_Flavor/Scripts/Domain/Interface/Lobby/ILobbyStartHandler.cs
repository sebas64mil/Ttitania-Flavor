using System.Threading.Tasks;

public interface ILobbyStartHandler
{
    Task StartGame(StartGameContext context);
}