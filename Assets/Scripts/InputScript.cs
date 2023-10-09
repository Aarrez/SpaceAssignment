using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private KeybindActions keybindActions;

    public static UnityAction<InputAction.CallbackContext> MovementAction, MouseMovement;

    private void Awake()
    {
        keybindActions = new KeybindActions();
    }

    private void OnEnable()
    {
        keybindActions.Player.ForwardBackwards.performed += ctx =>
        {
            MovementAction?.Invoke(ctx);
        };
        keybindActions.Player.ForwardBackwards.canceled += ctx =>
        {
            MovementAction?.Invoke(ctx);
        };

        keybindActions.Player.MouseLook.performed += ctx =>
            MouseMovement?.Invoke(ctx);
        
        
        keybindActions.Player.MouseLook.canceled += ctx => 
              MouseMovement?.Invoke(ctx);
        
        keybindActions.Enable();
    }

    private void OnDisable()
    {
        keybindActions.Disable();
    }
}