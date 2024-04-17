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

    protected override void EnemyAttackAtCertainRange()
    {
        if (GetIsPlayerSeen() && statusEffect != StatusEffect.Freeze)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange ||
                (isAttacking && Vector3.Distance(transform.position, playerTransform.position) <= attackRange + extendedAttackRange))
            {
                LookAtPlayer();
                if (!isAttacking)
                {
                    isAttacking = true;
                }
                else if (isAttacking && currentAttackTimer >= attackWaitTime)
                {
                    Attack();
                    ResetAttackWaitTime();
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
        if (playerTransform != null && statusEffect != StatusEffect.Freeze)
        {
            Vector3 flyingDistination = new Vector3(playerTransform.position.x, maxFlyingHeight + playerTransform.position.y, playerTransform.position.y);
            transform.position = Vector3.MoveTowards(transform.position, flyingDistination, currentMovementSpeed * Time.deltaTime);
        }
    }

    public override void Freeze()
    {
        base.Freeze();
        gameObject.AddComponent<Rigidbody>();
    }
}
