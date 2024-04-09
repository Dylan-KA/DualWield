using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float baseSpeed;
    [SerializeField] protected float speedMult = 1;
    [SerializeField] protected float baseDamage;
    [SerializeField] protected bool isFiredByEnemy = false;
    protected Rigidbody rigidbody;

    public void SetProjectileProperties(float baseDamage, float baseSpeed, float speedMult = 1)
    {
        this.baseSpeed = baseSpeed;
        this.baseDamage = baseDamage;
        this.speedMult = speedMult;
    }

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = isFiredByEnemy ? baseSpeed * speedMult * transform.forward : baseSpeed * speedMult * -transform.forward;
        gameObject.layer = isFiredByEnemy ? 9 : 8;
    }

    protected virtual void Update()
    {
        // move projectile
        //Debug.Log(speed);
        //transform.Translate(0,0, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnImpact(other);
        Destroy(gameObject);
    }

    protected abstract void OnImpact(Collider other);
}
