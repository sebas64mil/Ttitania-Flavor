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
            transform.position = targetHoldPoint.position;

            transform.rotation = targetHoldPoint.rotation;
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