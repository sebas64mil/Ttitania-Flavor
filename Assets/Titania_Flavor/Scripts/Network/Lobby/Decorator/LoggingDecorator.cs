using System;
using System.Threading.Tasks;
using UnityEngine;

public class LoggingDecorator : LobbyDecorator
{
    public LoggingDecorator(ILobbyStartHandler next)
        : base(next)
    {
    }

    public override async Task StartGame(StartGameContext context)
    {
        try
        {
            Debug.Log("Iniciando conexión...");

            await base.StartGame(context);

            Debug.Log($"[Lobby] Conexión exitosa en modo {context.Mode}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[Lobby] Error al iniciar juego: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
        
        Debug.Log($"Iniciando juego en modo {context.Mode} con sesión '{context.RoomCode}' y código de sala '{context.RoomCode}'");
    }
}