using Fusion;
using System.Threading.Tasks;

public class CallbackDecorator : LobbyDecorator
{
    public CallbackDecorator(ILobbyStartHandler next)
        : base(next)
    {
    }

    public override async Task StartGame(StartGameContext context)
    {
        await base.StartGame(context);

        if (context.Mode == GameMode.Host)
        {
            LobbyEvents.OnRoomCreated?.Invoke(context.RoomCode, context.RoomCode);
        }
    }
}