using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private KeybindActions keybindActions;

    public static UnityAction<InputAction.CallbackContext> KMovementAction;
    public static UnityAction<InputAction.CallbackContext> CMovementAction;
    public static UnityAction<InputAction.CallbackContext> MouseMovement;
    public static UnityAction<InputAction.CallbackContext> ControllerMovement;
    private void Awake()
    {
        keybindActions = new KeybindActions();
    }

    private void OnEnable()
    {
        //Sending keyboard shift and control context
        keybindActions.Player.KForwardBackwards.performed += ctx =>
            KMovementAction?.Invoke(ctx);
        
        keybindActions.Player.KForwardBackwards.canceled += ctx =>
            KMovementAction?.Invoke(ctx);
        
        //Sending controller left and right trigger context
        keybindActions.Player.KForwardBackwards.performed += ctx =>
            KMovementAction?.Invoke(ctx);
        
        keybindActions.Player.KForwardBackwards.canceled += ctx =>
            KMovementAction?.Invoke(ctx);
        
        //Sending mouse position context
        keybindActions.Player.MouseLook.performed += ctx =>
            MouseMovement?.Invoke(ctx);
        
        keybindActions.Player.MouseLook.canceled += ctx => 
              MouseMovement?.Invoke(ctx);

        //Sending controller stick context
        keybindActions.Player.ControllerLook.performed += ctx => 
            ControllerMovement?.Invoke(ctx);
        
        keybindActions.Player.ControllerLook.canceled += ctx => 
            ControllerMovement?.Invoke(ctx);
        
        keybindActions.Enable();
    }
    

    private void OnDisable()
    {
        keybindActions.Disable();
    }
}