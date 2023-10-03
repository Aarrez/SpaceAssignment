using CustomDebugger;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float maxValue = 2f;
    private Vector3 mousePos;

    private bool doTask = true;
    private void Awake()
    {
        InputScript.MouseMovement += GetMouseMovement;
    }
    private float time = 0;
    private void GetMouseMovement(InputAction.CallbackContext ctx)
    {
        Vector2 tempVector2 = ctx.ReadValue<Vector2>();
        mousePos = new Vector3(tempVector2.x, tempVector2.y, 10f);
        
        if(!doTask) return;
        doTask = false;
        UpdatePointer().Forget();
    }

    private async UniTaskVoid UpdatePointer()
    {
        time = 0;
        while (time < maxValue)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,mousePos, time);
            time += 0.1f;
            await UniTask.WaitForFixedUpdate();
        }
        doTask = true;

    }
}
