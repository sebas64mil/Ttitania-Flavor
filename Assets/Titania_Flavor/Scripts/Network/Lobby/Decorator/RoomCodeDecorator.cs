using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public class RoomCodeDecorator : LobbyDecorator
{
    public RoomCodeDecorator(ILobbyStartHandler next)
        : base(next)
    {
    }

    public override async Task StartGame(StartGameContext context)
    {
        if (context.Mode == GameMode.Host)
        {
            context.RoomCode = Random.Range(1000, 9999).ToString();
            Debug.Log($"[Lobby] HOST creado con código: {context.RoomCode}");
        }
        else
        {
            if (string.IsNullOrEmpty(context.RoomCode))
            {
                Debug.LogError("No se puede unir sin un código de sala válido");
                return;
            }

            Debug.Log($"[Lobby] CLIENT intentando unirse a sala: {context.RoomCode}");
        }

        await base.StartGame(context);
    }
}