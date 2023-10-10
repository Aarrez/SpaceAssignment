using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float health = 10f;
    [SerializeField] private float damageTick = 0.5f;
    [SerializeField] private float speed = 10f;

    private new Rigidbody rigidbody;

    private UniTask uniTask;
    private readonly CancellationTokenSource token = new CancellationTokenSource();

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        MoveMissileForward().Preserve();
    }

    private async UniTask MoveMissileForward()
    {
        while (health >= 0f)
        {
            if (token.Token.IsCancellationRequested) return;
            rigidbody.velocity = transform.forward * speed * Time.fixedDeltaTime;
            await UniTask.WaitForFixedUpdate(token.Token);
            health -= damageTick;
        } 
        DestroyTheProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyTheProjectile(other);
    }

    private void DestroyTheProjectile()
    {
        token.Cancel();
        Destroy(gameObject);
    }
    
    private void DestroyTheProjectile(Collider other)
    {
        token.Cancel();
        Destroy(other.transform.parent.gameObject);
        Destroy(gameObject);
    }
}
