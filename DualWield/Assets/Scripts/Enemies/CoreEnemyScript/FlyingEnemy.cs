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
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
            {
                LookAtPlayer();
                if (!isAttacking)
                {
                    isAttacking = true;
                    if (currentAttackTimer <= 0)
                    {
                        Attack();
                    }
                }
                else if (isAttacking && currentAttackTimer <= 0)
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

    protected override void MoveTowardsTarget()
    {
        if (playerTransform != null && statusEffect != StatusEffect.Freeze && navAgent.enabled == true)
        {
            SetMovementSpeed();
            navAgent.SetDestination(playerTransform.position);
        }
    }

    public override void Freeze()
    {
        base.Freeze();
        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().isTrigger = false;
    }
}
