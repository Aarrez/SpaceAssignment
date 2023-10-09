using CustomDebugger;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    
    private Vector3 cursorPos;
    private void Awake()
    {
        InputScript.ControllerMovement += ctx =>
        {
            cursorPos = (Vector3)ctx.ReadValue<Vector2>() + new Vector3(0f, 0f, distance);
            GetMouseMovement();
        };
    }
    private void GetMouseMovement()
    {
        transform.localPosition = cursorPos;
    }
}
