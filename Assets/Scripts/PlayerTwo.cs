using CustomDebugger;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwo : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float maxValue = 1f;
    private float time;
    
    private GameObject pointer;

    private Vector3 moveDirection;
    private CharacterController controller;

    private UniTask rotTask;
    
    private float moveCtx;

    private bool doOperation = true;
    private void Awake()
    {
        InputScript.MovementAction += PlayerMovement;
        InputScript.MouseMovement += MouseMovement;
        controller = GetComponent<CharacterController>();
        pointer = GameObject.FindWithTag("Pointer");
    }
    
    private void PlayerMovement(InputAction.CallbackContext ctx)
    {
        moveCtx = ctx.ReadValue<float>();
    }

    private void MouseMovement(InputAction.CallbackContext ctx)
    {
        if (!doOperation) return;
        doOperation = false;
        PlayerRotateToPointer().Forget();
        
    }
    private async UniTask PlayerRotateToPointer()
    {
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
        time = 0;
        while (time < maxValue)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            await UniTask.WaitForFixedUpdate();
            time += 0.1f;
        }
        doOperation = true;
    }
    
    private void Update()
    {
        moveDirection = pointer.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        
        controller.Move(moveDirection * (moveSpeed * moveCtx));
    }
}