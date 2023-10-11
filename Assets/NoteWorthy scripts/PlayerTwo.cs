using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerTwo : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    
    [SerializeField] private float moveSpeed = 1f;
    private float time;
    
    private GameObject pointer;

    private Vector3 moveDirection;
    private Rigidbody rigidbody;
    
    private float moveCtx;

    private bool doOperation = true;
    private bool contextBool = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InputScript.CMovementAction += PlayerMovement;
        InputScript.ControllerMovement += ctx =>
        {
            contextBool = ctx.canceled;
            RotationMovement();
        };
        
        SceneManager.activeSceneChanged += (current, next) =>
        {
            rigidbody.velocity = Vector3.zero;
            transform.position = Vector3.zero;
        };

        InputScript.CAction += ShootProjectile;
            
        rigidbody = GetComponent<Rigidbody>();
        pointer = GetComponentInChildren<ControllerPointer>().gameObject;
    }
    
    private void FixedUpdate()
    {
        rigidbody.velocity += transform.forward * (moveSpeed * moveCtx);
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
        moveDirection = pointer.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
        rigidbody.rotation = lookRotation;
        await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
        doOperation = true;
    }
    
    
    private void ShootProjectile()
    {
        projectile.GetComponent<Rigidbody>().velocity = rigidbody.velocity;
        Instantiate(projectile.gameObject, transform.position, transform.rotation);
    }

    
}