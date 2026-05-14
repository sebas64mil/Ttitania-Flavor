using Fusion;
using UnityEngine;

[RequireComponent(typeof(NetworkCharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    private NetworkCharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {


        if (GetInput(out NetworkInputData data))
        {
            Vector3 moveDirection = data.Direction;

            if (moveDirection.sqrMagnitude > 1f)
            {
                moveDirection.Normalize();
            }

            characterController.Move(
                moveDirection * moveSpeed * Runner.DeltaTime
            );
        }
    }
}