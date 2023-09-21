using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private GameObject pointer;

    private Vector3 moveDirection;
    private CharacterController controller;

    private readonly CancellationTokenSource cancelToken = new CancellationTokenSource();

    private float moveCtx;

    private bool doOperation = true;
    private void Awake()
    {
        InputScript.MovementAction += PlayerMovement;
        InputScript.MouseMovement += MouseMovement;
        controller = GetComponent<CharacterController>();
        pointer = GameObject.FindWithTag("Pointer");
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void PlayerMovement(InputAction.CallbackContext ctx)
    {
        moveCtx = ctx.ReadValue<float>();
    }

    private void MouseMovement(InputAction.CallbackContext ctx)
    {
        
    }

    private float time;
    
    private async void FixedUpdate()
    {
        moveDirection = pointer.transform.position - transform.position;
        moveDirection = moveDirection.normalized;
        time = 0;
        var lookRotation = Quaternion.LookRotation(moveDirection);
        while (transform.rotation != lookRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            await UniTask.WaitForFixedUpdate();
            time += 0.01f;
        }

        controller.Move(moveDirection * (moveSpeed * moveCtx));
    }

    private void OnDestroy()
    {
        cancelToken.Dispose();
    }


}