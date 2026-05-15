using Fusion;
using UnityEngine;

public class GrabbableObject : NetworkBehaviour, IGrabbable
{
    [Networked]
    [HideInInspector]
    public NetworkBool IsGrabbed { get; set; }

    [Networked]
    [HideInInspector]
    public PlayerRef Holder { get; set; }

    [SerializeField]
    private float followSpeed = 25f;

    private Rigidbody rb;

    private Transform targetHoldPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void FixedUpdateNetwork()
    {
        if (IsGrabbed && targetHoldPoint != null)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetHoldPoint.position,
                Runner.DeltaTime * followSpeed
            );

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetHoldPoint.rotation,
                Runner.DeltaTime * followSpeed
            );
        }
    }

    public bool CanGrab()
    {
        return Holder == PlayerRef.None;
    }

    public void Grab(PlayerGrabber player)
    {
        if (!CanGrab())
        {
            return;
        }

        Holder = player.Object.InputAuthority;

        IsGrabbed = true;

        rb.isKinematic = true;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        targetHoldPoint = player.HoldPoint;
    }

    public void Drop()
    {
        Holder = PlayerRef.None;

        IsGrabbed = false;

        targetHoldPoint = null;

        rb.isKinematic = false;
    }
}