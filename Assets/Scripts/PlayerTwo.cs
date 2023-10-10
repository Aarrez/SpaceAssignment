using CustomDebugger;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwo : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float maxValue = 0.5f;
    private float time;
    
    private GameObject pointer;

    private Vector3 moveDirection;
    private CharacterController controller;

    private UniTask rotTask;
    
    private float moveCtx;

    private bool doOperation = true;
    private bool contextBool = true;

    private InputAction.CallbackContext context;
    private void Awake()
    {
        InputScript.CMovementAction += PlayerMovement;
        InputScript.ControllerMovement += ctx =>
        {
            context = ctx;
            contextBool = context.canceled;
            RotationMovement();
        };

        InputScript.CAction += ShootProjectile;
            
        controller = GetComponent<CharacterController>();
        pointer = GameObject.FindWithTag("Pointer");
    }

    private void ShootProjectile()
    {
        Instantiate(projectile.gameObject, transform.position, transform.rotation);
    }
    
    private void PlayerMovement(InputAction.CallbackContext ctx)
    {
        moveCtx = ctx.ReadValue<float>();
    }

    private void RotationMovement()
    {
        if (!doOperation) return;
        doOperation = false;
        StartStopRotation().Forget();
    }
    private async UniTaskVoid StartStopRotation()
    {
        while (!contextBool)
        {
            await PlayerRotateToPointer();
        }
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