using UnityEngine;

public class BoostShip : MonoBehaviour
{
    [SerializeField] private float foreceAmount = 100f;
    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.AddForce(other.transform.forward * foreceAmount, ForceMode.Impulse);
    }
}