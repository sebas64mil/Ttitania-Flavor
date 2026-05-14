using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SessionListUI : MonoBehaviour
{
    [SerializeField]
    private Transform sessionListContainer;

    [SerializeField]
    private SessionItemUI sessionItemPrefab;

    [SerializeField]
    private string sceneNameToLoad = "Lobby";

    private List<SessionItemUI> instantiatedItems = new();

    private void OnEnable()
    {
        LobbyEvents.OnSessionListUpdated += DisplaySessionList;
    }

    private void OnDisable()
    {
        LobbyEvents.OnSessionListUpdated -= DisplaySessionList;
    }

    private void DisplaySessionList(List<SessionInfo> sessions)
    {
        ClearSessionList();

        if (sessions == null || sessions.Count == 0)
        {
            Debug.LogWarning("[SessionListUI] No hay sesiones disponibles");
            return;
        }

        foreach (var session in sessions)
        {
            SessionItemUI item = Instantiate(sessionItemPrefab, sessionListContainer);
            item.Initialize(session, sceneNameToLoad);
            instantiatedItems.Add(item);
        }

        Debug.Log($"[SessionListUI] Se mostraron {instantiatedItems.Count} sesiones");
    }

    private void ClearSessionList()
    {
        foreach (var item in instantiatedItems)
        {
            Destroy(item.gameObject);
        }
        instantiatedItems.Clear();
    }

    public void RefreshSessionList()
    {
        Debug.Log("[SessionListUI] Actualizando lista de sesiones...");
    }
}