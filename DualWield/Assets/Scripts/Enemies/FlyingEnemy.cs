using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : BaseEnemy
{
    [SerializeField] protected float maxFlyingHeight;

    protected override void Start()
    {
        base.Start();
    }

    protected override void MoveTowardsTarget()
    {
        if (playerTransform != null && statusEffect != StatusEffect.Freeze)
        {
            Vector3 flyingDistination = new Vector3(playerTransform.position.x, maxFlyingHeight + playerTransform.position.y, playerTransform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, flyingDistination, movementSpeed * Time.deltaTime);
        }
    }

    public override void Freeze()
    {
        base.Freeze();
        gameObject.AddComponent<Rigidbody>();
    }
}
