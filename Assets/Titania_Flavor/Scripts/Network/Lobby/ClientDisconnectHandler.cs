using UnityEngine;
using Fusion;

public class ClientDisconnectHandler : MonoBehaviour
{
    private NetworkRunner runner;
    private bool isShuttingDown;

    private void Start()
    {
        HideLeaveGamePanel();
        isShuttingDown = false;
    }

    private void OnEnable()
    {
        LobbyEvents.OnFinishedLoading += ShowLeaveGamePanel;
        LobbyEvents.OnDisconnected += HandleDisconnected;
    }

    private void OnDisable()
    {
        LobbyEvents.OnFinishedLoading -= ShowLeaveGamePanel;
        LobbyEvents.OnDisconnected -= HandleDisconnected;
    }

    private void ShowLeaveGamePanel()
    {
        runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
            return;

        if (!runner.IsClient)
            return;

        if (isShuttingDown)
            return;

        if (LobbyUIReferences.Instance != null && LobbyUIReferences.Instance.LeaveGamePanel != null)
        {
            LobbyUIReferences.Instance.LeaveGamePanel.SetActive(true);
        }
    }

    private void HideLeaveGamePanel()
    {
        if (LobbyUIReferences.Instance != null && LobbyUIReferences.Instance.LeaveGamePanel != null)
        {
            LobbyUIReferences.Instance.LeaveGamePanel.SetActive(false);
        }
    }

    public void OnLeaveGameButtonClicked()
    {
        Debug.Log("[ClientDisconnectHandler] Cliente solicitando salida del juego");

        LeaveGame();
    }

    public void LeaveGame()
    {
        runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
        {
            Debug.LogWarning("[ClientDisconnectHandler] No hay NetworkRunner activo");
            return;
        }

        if (!runner.IsClient)
        {
            Debug.LogWarning("[ClientDisconnectHandler] Solo los clientes pueden salir con este método");
            return;
        }

        Debug.Log("[ClientDisconnectHandler] Desconectando cliente del juego");

        isShuttingDown = true;
        LobbyNetwork.Instance.Shutdown();
    }

    private void HandleDisconnected()
    {
        runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
            return;

        if (!runner.IsClient)
            return;

        HideLeaveGamePanel();
        isShuttingDown = false;
    }
}