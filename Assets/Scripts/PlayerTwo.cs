using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwo : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    
    [SerializeField] private float moveSpeed = 1f;
    private float time;
    
    private GameObject pointer;

    private Vector3 moveDirection;
    private new Rigidbody rigidbody;
    
    private float moveCtx;

    private bool doOperation = true;
    private bool contextBool = true;

    private void Awake()
    {
        InputScript.CMovementAction += PlayerMovement;
        InputScript.ControllerMovement += ctx =>
        {
            contextBool = ctx.canceled;
            RotationMovement();
        };

        InputScript.CAction += ShootProjectile;
            
        rigidbody = GetComponent<Rigidbody>();
        pointer = GetComponentInChildren<ControllerPointer>().gameObject;
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
        rigidbody.rotation = lookRotation;
        await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        doOperation = true;
    }
    
    private void Update()
    {
        moveDirection = pointer.transform.position - transform.position;
        moveDirection = moveDirection.normalized;

        rigidbody.velocity += moveDirection * (moveSpeed * moveCtx);
    }
}