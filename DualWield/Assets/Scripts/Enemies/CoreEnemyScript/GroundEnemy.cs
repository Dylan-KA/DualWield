using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : BaseEnemy
{
    protected NavMeshAgent navAgent;

    protected override void Start()
    {
        base.Start();
        try
        {
            navAgent = GetComponent<NavMeshAgent>();
        }
        catch
        {
            Debug.Log("NavMeshAgent cannot be found");
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyAttackAtCertainRange()
    {
        if (GetIsPlayerSeen() && statusEffect != StatusEffect.Freeze)
        {
            Debug.Log("Player seen");
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange ||
                (isAttacking && Vector3.Distance(transform.position, playerTransform.position) <= attackRange + extendedAttackRange))
            {
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
                ResetAttack();
                MoveTowardsTarget();
            }
        }
    }

    protected override void MoveTowardsTarget()
    {
        navAgent.SetDestination(playerTransform.position);
    }

    // Sets the movement speed based on how frzozen the enemy is
    protected virtual void SetMovementSpeed()
    {
        navAgent.speed = baseMovementSpeed * (1 - (FreezePercent / 100));
    }
}
