using CustomDebugger;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    
    private Vector3 mousePos;
    private void Awake()
    {
        InputScript.MouseMovement += ctx =>
        {
            mousePos = (Vector3)ctx.ReadValue<Vector2>() + new Vector3(0f, 0f, 10f);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = distance;
            GetMouseMovement();
        };
    }
    private void GetMouseMovement()
    {
        transform.position = mousePos;
    }
}
