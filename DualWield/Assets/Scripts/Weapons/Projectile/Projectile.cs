using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float baseSpeed;
    [SerializeField] protected float speedMult = 1;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected bool isFiredByEnemy = false;
    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.layer = isFiredByEnemy ? 9 : 8;
        ProjectileMovement();
    }

    protected virtual void Update()
    {
        // move projectile
        // Debug.Log(speed);
        // transform.Translate(0,0, -speed * Time.deltaTime);
    }

    protected virtual void ProjectileMovement()
    {
        rb.velocity = isFiredByEnemy ? baseSpeed * speedMult * transform.forward : baseSpeed * speedMult * -transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnImpact(other);
        Destroy(gameObject);
    }

    protected abstract void OnImpact(Collider other);
}
