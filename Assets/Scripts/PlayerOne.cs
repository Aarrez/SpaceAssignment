using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float maxValue = 1f;
    private float time;
    
    private GameObject pointer;

    private Vector3 moveDirection;
    private new Rigidbody rigidbody;
    
    
    private float moveCtx;

    private bool doOperation = true;
    private bool contextBool = true;
    private void Awake()
    {
        InputScript.KMovementAction += PlayerMovement;
        InputScript.RightMouse += StartStopRotation;
        InputScript.KAction += ShootProjectile;

        rigidbody = GetComponent<Rigidbody>();
        pointer = GetComponentInChildren<MousePointer>().gameObject;
    }
    
    private void ShootProjectile()
    {
        Instantiate(projectile.gameObject, transform.position, transform.rotation);
    }
    
    private void PlayerMovement(InputAction.CallbackContext ctx)
    {
        moveCtx = ctx.ReadValue<float>();
    }
    
    private async void StartStopRotation(InputAction.CallbackContext ctx)
    {
        contextBool = ctx.canceled;
        while (!contextBool)
        {
             MouseMovement();
             await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        }
    }

    private void MouseMovement()
    {
        if (!doOperation) return;
        doOperation = false;
        PlayerRotateToPointer().Forget();
    }
    private async UniTask PlayerRotateToPointer()
    {
        moveDirection = pointer.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
        rigidbody.rotation =
            Quaternion.RotateTowards(rigidbody.rotation, lookRotation, maxValue);
        
        await UniTask.WaitForFixedUpdate();
        doOperation = true;
    }
    
    private void FixedUpdate()
    {
        rigidbody.velocity += transform.forward * (moveSpeed * moveCtx);
    }
}