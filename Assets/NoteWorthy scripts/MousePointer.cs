using CustomDebugger;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    [SerializeField] private float distance = 1f;

    private Camera camera;

    private Vector3 cursorPos;
    private void Awake()
    {
        camera = transform.parent.GetComponentInChildren<Camera>();
        InputScript.MouseMovement += ctx =>
        {
            cursorPos = (Vector3)ctx.ReadValue<Vector2>() + new Vector3(0f, 0f, distance);
            cursorPos = camera.ScreenToWorldPoint(cursorPos);
            GetMouseMovement();
        };
    }
    private void GetMouseMovement()
    {
        transform.position = cursorPos;
    }
}