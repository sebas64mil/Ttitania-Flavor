using TMPro;
using UnityEngine;

public class LobbyPlayerInfoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playersText;

    private void OnEnable()
    {
        LobbyEvents.OnPlayerCountChanged += UpdateUI;
    }

    private void OnDisable()
    {
        LobbyEvents.OnPlayerCountChanged -= UpdateUI;
    }

    private void UpdateUI(int playerCount)
    {
        playersText.text = $"Players Connected: {playerCount}";
    }
}