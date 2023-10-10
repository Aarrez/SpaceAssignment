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
    public static UnityAction KAction, CAction;
    
    private void Awake()
    {
        keybindActions = new KeybindActions();
    }

    private void OnEnable()
    {
        //Sending keyboard shift and control context
        keybindActions.PlayerOne.KForwardBackwards.performed += ctx =>
            KMovementAction?.Invoke(ctx);
        
        keybindActions.PlayerOne.KForwardBackwards.canceled += ctx =>
            KMovementAction?.Invoke(ctx);
        
        //Sending mouse position context
        keybindActions.PlayerOne.MouseLook.performed += ctx =>
            MouseMovement?.Invoke(ctx);
        
        keybindActions.PlayerOne.MouseLook.canceled += ctx => 
              MouseMovement?.Invoke(ctx);
        
        //Invoke mouse event for shooting
        keybindActions.PlayerOne.Shoot.performed += ctx =>
            KAction?.Invoke();
        
        /* ----------------------------------------------------------------------- */
        //Sending controller left and right trigger context
        keybindActions.PlayerTwo.CForwardBackwards.performed += ctx =>
            CMovementAction?.Invoke(ctx);
        
        keybindActions.PlayerTwo.CForwardBackwards.canceled += ctx =>
            CMovementAction?.Invoke(ctx);

        //Sending controller stick context
        keybindActions.PlayerTwo.ControllerLook.performed += ctx => 
            ControllerMovement?.Invoke(ctx);
        
        keybindActions.PlayerTwo.ControllerLook.canceled += ctx => 
            ControllerMovement?.Invoke(ctx);
        
        //Invoke controller event for Shooting
        keybindActions.PlayerTwo.Shoot.performed += ctx =>
            CAction?.Invoke();
        
        keybindActions.Enable();
    }
    

    private void OnDisable()
    {
        keybindActions.Disable();
    }
}