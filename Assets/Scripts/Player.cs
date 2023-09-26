using CustomDebugger;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxValue = 1f;
    private float time;
    
    private GameObject pointer;

    private Vector3 moveDirection;
    private CharacterController controller;

    private readonly CancellationTokenSource cancelToken = new CancellationTokenSource();

    private Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
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
        new Debugger(moveDirection);
    }
    private async UniTask PlayerRotateToPointer()
    {  
        time = 0;
        var lookRotation = Quaternion.LookRotation(moveDirection);
        while (time < maxValue)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            await UniTask.WaitForFixedUpdate();
            time += 0.1f;
        }
        doOperation = true;
    }
    
    private void FixedUpdate()
    {
        moveDirection = pointer.transform.position - transform.position;
        moveDirection = moveDirection.normalized;

        controller.Move(moveDirection * (moveSpeed * moveCtx));
    }

    private void OnDestroy()
    {
        cancelToken.Dispose();
    }


}