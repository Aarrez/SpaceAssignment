using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerOne : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float maxValue = 1f;
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
        InputScript.KMovementAction += PlayerMovement;
        InputScript.RightMouse += StartStopRotation;
        InputScript.KAction += ShootProjectile;

        SceneManager.activeSceneChanged += (current, next) =>
        {
            rigidbody.velocity = Vector3.zero;
            transform.position = Vector3.zero;
        };
           
        

        rigidbody = GetComponent<Rigidbody>();
        pointer = GetComponentInChildren<MousePointer>().gameObject;
    }
    
    private void FixedUpdate()
    {
        rigidbody.velocity += transform.forward * (moveSpeed * moveCtx);
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
    
    private void ShootProjectile()
    {
        projectile.GetComponent<Rigidbody>().velocity = rigidbody.velocity;
        Instantiate(projectile.gameObject, transform.position, transform.rotation);
    }
}