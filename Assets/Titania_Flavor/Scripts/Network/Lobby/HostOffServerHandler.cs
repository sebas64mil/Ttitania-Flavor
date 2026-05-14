using UnityEngine;
using Fusion;

public class HostOffServerHandler : MonoBehaviour
{
    private NetworkRunner runner;

    private void Start()
    {
        HideDisconnectPanel();
    }

    private void OnEnable()
    {
        LobbyEvents.OnFinishedLoading += ShowDisconnectPanel;
        LobbyEvents.OnDisconnected += HandleDisconnected;
    }

    private void OnDisable()
    {
        LobbyEvents.OnFinishedLoading -= ShowDisconnectPanel;
        LobbyEvents.OnDisconnected -= HandleDisconnected;
    }

    private void ShowDisconnectPanel()
    {
        runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
            return;

        if (!runner.IsServer)
            return;

        if (LobbyUIReferences.Instance != null && LobbyUIReferences.Instance.HostDisconnectPanel != null)
        {
            LobbyUIReferences.Instance.HostDisconnectPanel.SetActive(true);
            Debug.Log("[HostOffServerHandler] Panel de desconexión visible solo para el host");
        }
    }

    private void HideDisconnectPanel()
    {
        if (LobbyUIReferences.Instance != null && LobbyUIReferences.Instance.HostDisconnectPanel != null)
        {
            LobbyUIReferences.Instance.HostDisconnectPanel.SetActive(false);
        }
    }

    public void OnDisconnectServerButtonClicked()
    {
        Debug.Log("[HostOffServerHandler] Host solicitando cierre del servidor");
        DisconnectServer();
    }

    public void DisconnectServer()
    {
        runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
        {
            Debug.LogWarning("[HostOffServerHandler] No hay NetworkRunner activo");
            return;
        }

        if (!runner.IsServer)
        {
            Debug.LogWarning("[HostOffServerHandler] Solo el host puede desconectar el servidor");
            return;
        }

        Debug.Log("[HostOffServerHandler] Desconectando servidor y todos los jugadores");

        LobbyNetwork.Instance.Shutdown();
    }

    private void HandleDisconnected()
    {
        runner = LobbyNetwork.Instance.GetRunner();

        if (runner == null)
            return;

        if (!runner.IsServer)
            return;

        Debug.Log("[HostOffServerHandler] Servidor desconectado. Ocultando panel");

        HideDisconnectPanel();
    }
}
