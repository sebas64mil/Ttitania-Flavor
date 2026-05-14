using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public class DisconnectServerDecoretor : LobbyDecorator
{
    public DisconnectServerDecoretor(ILobbyStartHandler next)
        : base(next)
    {
    }

    public override async Task StartGame(StartGameContext context)
    {
        await base.StartGame(context);

        Debug.Log("[DisconnectServerDecoretor] Juego iniciado correctamente");
    }
}
