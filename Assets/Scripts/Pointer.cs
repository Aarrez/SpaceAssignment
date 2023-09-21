using CustomDebugger;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    private Vector3 mousePos;
    private void Awake()
    {
        InputScript.MouseMovement += GetMouseMovement;
    }
    private float time = 0;
    private void GetMouseMovement(InputAction.CallbackContext ctx)
    {
        Vector2 tempVector2 = ctx.ReadValue<Vector2>().normalized;
        mousePos = new Vector3(tempVector2.x, tempVector2.y, 10f);
        
        while (transform.localPosition != mousePos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,mousePos, time);
            time += 0.1f;
        }
        time = 0;

    }

}
