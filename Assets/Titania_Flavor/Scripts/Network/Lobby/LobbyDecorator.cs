using System.Threading.Tasks;

public abstract class LobbyDecorator : ILobbyStartHandler
{
    protected ILobbyStartHandler next;

    protected LobbyDecorator(ILobbyStartHandler nextHandler)
    {
        next = nextHandler;
    }

    public virtual async Task StartGame(StartGameContext context)
    {
        await next.StartGame(context);
    }
}