using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Health goes down by DamageTick every FixedUpdate")] [SerializeField]
    private float health = 10f;
    [SerializeField] private float damageTick = 0.5f;
    [SerializeField] private float speed = 10f;

    public Rigidbody rigidbody;
    private readonly CancellationTokenSource token = new CancellationTokenSource();

    private UniTask uniTask;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        MoveMissileForward().Preserve();
    }

    private void OnDestroy()
    {
        token.Cancel();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyTheProjectile(other);
    }

    private async UniTask MoveMissileForward()
    {
        while (health >= 0f || token.IsCancellationRequested)
        {
            rigidbody.velocity += transform.forward * speed;
            await UniTask.WaitForFixedUpdate(token.Token);
            health -= damageTick;
        }
        DestroyTheProjectile();
    }

    private void DestroyTheProjectile()
    {
        token.Cancel();
        Destroy(gameObject);
    }

    private void DestroyTheProjectile(Collider other)
    {
        token.Cancel();
        other.transform.parent.GetComponent<Asteriod>().AsteriodTakeDamage();
        Destroy(gameObject);
    }
}