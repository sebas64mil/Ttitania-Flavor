using UnityEngine;
using TMPro;
using Fusion;

public class SessionItemUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text sessionNameText;

    [SerializeField]
    private TMP_Text playerCountText;

    private SessionInfo currentSession;
    private string sceneToLoad;

    public void Initialize(SessionInfo session, string sceneName)
    {
        currentSession = session;
        sceneToLoad = sceneName;

        if (sessionNameText != null)
        {
            sessionNameText.text = session.Name;
        }

        if (playerCountText != null)
        {
            playerCountText.text = $"{session.PlayerCount}/{session.MaxPlayers}";
        }

        Debug.Log($"[SessionItemUI] Sesión inicializada - Nombre: {session.Name}");
    }

    public void OnJoinButtonClicked()
    {
        Debug.Log($"[SessionItemUI] Intentando unirse a: {currentSession.Name} (Código: {currentSession.Name})");

        LobbyEvents.OnSessionSelected?.Invoke(currentSession);

        LobbyNetwork.Instance.JoinGame(sceneToLoad, currentSession.Name);
    }
}