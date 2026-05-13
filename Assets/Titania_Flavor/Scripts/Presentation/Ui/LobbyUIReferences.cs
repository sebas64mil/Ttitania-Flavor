using UnityEngine;
using TMPro;

public class LobbyUIReferences : MonoBehaviour
{
    public static LobbyUIReferences Instance;

    [Header("Panels")]
    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject roomInfoPanel;

    [Header("Texts")]
    [SerializeField]
    private TMP_Text playerCountText;

    [SerializeField]
    private TMP_Text roomCodeText;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject LoadingPanel => loadingPanel;
    public GameObject RoomInfoPanel => roomInfoPanel;

    public TMP_Text PlayerCountText => playerCountText;
    public TMP_Text RoomCodeText => roomCodeText;
}