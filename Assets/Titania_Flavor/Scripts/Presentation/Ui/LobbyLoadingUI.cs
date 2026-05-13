using UnityEngine;

public class LobbyLoadingUI : MonoBehaviour
{
    private LobbyUIReferences ui;

    private void Start()
    {
        ui = LobbyUIReferences.Instance;
    }

    private void OnEnable()
    {
        LobbyEvents.OnStartHosting += ShowLoading;
        LobbyEvents.OnStartJoining += ShowLoading;
        LobbyEvents.OnFinishedLoading += HideLoading;
    }

    private void OnDisable()
    {
        LobbyEvents.OnStartHosting -= ShowLoading;
        LobbyEvents.OnStartJoining -= ShowLoading;
        LobbyEvents.OnFinishedLoading -= HideLoading;
    }

    private void ShowLoading()
    {
        ui.LoadingPanel.SetActive(true);
    }

    private void HideLoading()
    {
        ui.LoadingPanel.SetActive(false);
    }
}