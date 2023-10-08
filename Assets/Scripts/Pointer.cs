using CustomDebugger;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float maxValue = 2f;
    [SerializeField] private float radius = 1f;
    private InputAction.CallbackContext context;
    private Vector3 mousePos;
    private float time = 0f;
    
    private bool doTask = true;
    private void Awake()
    {
        InputScript.MouseMovement += ctx =>
        {
            context = ctx;
            mousePos = (Vector3)ctx.ReadValue<Vector2>() + new Vector3(1f, 1f, 10f);
            GetMouseMovement();
        };
    }
    private void GetMouseMovement()
    {
        transform.position = mousePos;
    }
    
    // private void GetMouseMovement(InputAction.CallbackContext ctx)
    // {
    //     Vector2 tempVector2 = ctx.ReadValue<Vector2>();
    //     new Debugger(tempVector2);
    //     mousePos = new Vector3(tempVector2.x, tempVector2.y, 10f);
    //     transform.localPosition = mousePos;
    //     // if(!doTask) return;
    //     // doTask = false;
    //     // UpdatePointer().Forget();
    // }

    private async UniTaskVoid UpdatePointer()
    {
        time = 0;
        while (time < maxValue)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,mousePos, time);
            time += 0.1f;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        doTask = true;

    }
}
