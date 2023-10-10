using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputScript : MonoBehaviour
{
    private KeybindActions keybindActions;
    private KeybindActions.PlayerOneActions playerOne;
    private KeybindActions.PlayerTwoActions playerTwo;
    

    public static UnityAction<InputAction.CallbackContext> KMovementAction;
    public static UnityAction<InputAction.CallbackContext> MouseMovement;
    public static UnityAction<InputAction.CallbackContext> RightMouse;
        
    public static UnityAction<InputAction.CallbackContext> CMovementAction;
    public static UnityAction<InputAction.CallbackContext> ControllerMovement;
    
    public static UnityAction KAction, CAction;
    public static UnityAction KStart, CStart;

    
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        keybindActions = new KeybindActions();
        playerOne = keybindActions.PlayerOne;
        playerTwo = keybindActions.PlayerTwo;
    }

    private void OnEnable()
    {
        //Sending keyboard shift and control context
        playerOne.KForwardBackwards.performed += ctx =>
            KMovementAction?.Invoke(ctx);
        
        playerOne.KForwardBackwards.canceled += ctx =>
            KMovementAction?.Invoke(ctx);
        
        //Sending mouse position context
        playerOne.MouseLook.performed += ctx =>
            MouseMovement?.Invoke(ctx);
        
        playerOne.MouseLook.canceled += ctx => 
              MouseMovement?.Invoke(ctx);
        
        //Invoke mouse event for shooting
        playerOne.Shoot.performed += ctx =>
            KAction?.Invoke();

        //Invoke right mouse button to rotate playerOne
        playerOne.RotatePlayer.performed += ctx =>
            RightMouse?.Invoke(ctx);
        
        playerOne.RotatePlayer.canceled += ctx =>
            RightMouse?.Invoke(ctx);
        
        //Keyboard start event
        playerOne.Start.performed += _ =>
            KStart?.Invoke();
        
        /* ----------------------------------------------------------------------- */
        //Sending controller left and right trigger context
        playerTwo.CForwardBackwards.performed += ctx =>
            CMovementAction?.Invoke(ctx);
        
        playerTwo.CForwardBackwards.canceled += ctx =>
            CMovementAction?.Invoke(ctx);

        //Sending controller stick context
        playerTwo.ControllerLook.performed += ctx => 
            ControllerMovement?.Invoke(ctx);
        
        playerTwo.ControllerLook.canceled += ctx => 
            ControllerMovement?.Invoke(ctx);
        
        //Invoke controller event for Shooting
        playerTwo.Shoot.performed += _ =>
            CAction?.Invoke();
        
        //Controller start event
        playerTwo.Start.performed += _ =>
            CStart?.Invoke();
        
        keybindActions.Enable();
    }
    

    private void OnDisable()
    {
        keybindActions.Disable();
    }
}