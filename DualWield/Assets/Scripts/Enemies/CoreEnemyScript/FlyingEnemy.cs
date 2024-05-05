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

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Floor") && statusEffect == StatusEffect.Freeze)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            TakeDamage(squashDamage);
        }
    }

    protected override void EnemyAttackAtCertainRange()
    {
        if (GetIsPlayerSeen() && statusEffect != StatusEffect.Freeze)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange ||
                isAttacking)
            {
                LookAtPlayer();
                if (!isAttacking)
                {
                    isAttacking = true;
                }
                else if (isAttacking && currentAttackTimer >= attackWaitTime)
                {
                    Attack();
                }
            }
            else
            {
                LookAtPlayer();
                ResetAttack();
                MoveTowardsTarget();
            }
        }
    }
    protected virtual void SetMovementSpeed()
    {
        currentMovementSpeed = baseMovementSpeed * (1 - (FreezePercent / 100));
    }
    protected override void MoveTowardsTarget()
    {
        if (playerTransform != null && statusEffect != StatusEffect.Freeze)
        {
            Vector3 flyingDistination = new (playerTransform.position.x, maxFlyingHeight + playerTransform.position.y, playerTransform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, flyingDistination, currentMovementSpeed * Time.deltaTime);
        }
    }

    public override void Freeze()
    {
        base.Freeze();
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().isTrigger = false;
    }
}
