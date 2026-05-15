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
            rb.MovePosition(targetHoldPoint.position);
            rb.MoveRotation(targetHoldPoint.rotation);
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

        targetHoldPoint = player.HoldPoint;


        if (!rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        rb.isKinematic = true;
    }

    public void Drop()
    {
        Holder = PlayerRef.None;

        IsGrabbed = false;

        targetHoldPoint = null;

        rb.isKinematic = false;
    }
}