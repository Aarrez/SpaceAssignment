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
            Debug.Log("happening");
            mousePos = (Vector3)ctx.ReadValue<Vector2>() + new Vector3(0f, 0f, distance);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            GetMouseMovement();
        };
    }
    private void GetMouseMovement()
    {
        transform.position = mousePos;
    }
}
