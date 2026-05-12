using TMPro;
using UnityEngine;

public class LoadingPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;

    [SerializeField] private GameObject roomPanel;

    [SerializeField] private TMP_Text loadingText;

    private void OnEnable()
    {
        LobbyEvents.OnStartHosting += ShowHosting;

        LobbyEvents.OnStartJoining += ShowJoining;

        LobbyEvents.OnFinishedLoading += HideLoading;
    }

    private void OnDisable()
    {
        LobbyEvents.OnStartHosting -= ShowHosting;

        LobbyEvents.OnStartJoining -= ShowJoining;

        LobbyEvents.OnFinishedLoading -= HideLoading;
    }

    private void ShowHosting()
    {
        loadingPanel.SetActive(true);

        loadingText.text = "Hosting...";
    }

    private void ShowJoining()
    {
        loadingPanel.SetActive(true);

        loadingText.text = "Joining...";
    }

    private void HideLoading()
    {
        loadingPanel.SetActive(false);
        roomPanel.SetActive(true);
    }
}