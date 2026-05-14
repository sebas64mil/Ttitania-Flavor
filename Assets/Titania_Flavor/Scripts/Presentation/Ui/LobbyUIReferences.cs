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

    [SerializeField]
    private GameObject hostDisconnectPanel;

    [SerializeField]
    private GameObject leaveGamePanel;

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
    public GameObject HostDisconnectPanel => hostDisconnectPanel;
    public GameObject LeaveGamePanel => leaveGamePanel;

    public TMP_Text PlayerCountText => playerCountText;
    public TMP_Text RoomCodeText => roomCodeText;
}