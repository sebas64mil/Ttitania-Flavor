using Fusion;
using UnityEngine;

public class PlayerGrabber : NetworkBehaviour
{
    [SerializeField]
    private Transform holdPoint;

    [SerializeField]
    private float interactDistance = 2f;

    [SerializeField]
    private float interactRadius = 0.5f;

    [SerializeField]
    private LayerMask interactLayer;

    private IGrabbable heldObject;

    public Transform HoldPoint => holdPoint;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            if (data.InteractPressed)
            {
                HandleInteract();
            }
        }
    }

    private void HandleInteract()
    {
        if (heldObject != null)
        {
            heldObject.Drop();
            heldObject = null;
            return;
        }

        Vector3 detectionPoint =
            holdPoint.position + holdPoint.forward * interactDistance;

        Collider[] colliders = Physics.OverlapSphere(
            detectionPoint,
            interactRadius,
            interactLayer
        );

        if (colliders.Length == 0)
        {
            return;
        }

        Collider closestCollider = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(
                holdPoint.position,
                collider.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = collider;
            }
        }

        if (closestCollider != null &&
            closestCollider.TryGetComponent(out IGrabbable grabbable))
        {
            if (!grabbable.CanGrab())
            {
                return;
            }

            heldObject = grabbable;

            heldObject.Grab(this);
        }
    }

    private void OnDrawGizmos()
    {
        if (holdPoint == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        Vector3 detectionPoint =
            holdPoint.position + holdPoint.forward * interactDistance;

        Gizmos.DrawWireSphere(
            detectionPoint,
            interactRadius
        );
    }
}