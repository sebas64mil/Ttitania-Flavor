using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public static event Action<Vector2> OnMoveInput;

    public static Vector2 MoveInput { get; private set; }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();

        OnMoveInput?.Invoke(MoveInput);
    }
}