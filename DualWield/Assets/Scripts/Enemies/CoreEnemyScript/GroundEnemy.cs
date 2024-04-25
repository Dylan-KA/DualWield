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
        if (navAgent.enabled != false)
        {
            navAgent.SetDestination(playerTransform.position);
        }
        else
        {
            StartCoroutine(ResetNavMesh());
        }
    }

    // Sets the movement speed based on how frzozen the enemy is
    protected virtual void SetMovementSpeed()
    {
        navAgent.speed = baseMovementSpeed * (1 - (FreezePercent / 100));
    }

    public void PushEnemy()
    {
        navAgent.enabled = false;
        StopCoroutine(ResetNavMesh());
    }

    private IEnumerator ResetNavMesh()
    {
        float delay = 0.65f;
        WaitForSeconds wait = new(delay);
        yield return wait;
        Ray ray = new(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1, obstructionMask))
        {
            if (NavMesh.SamplePosition(hit.point, out _, 1.0f, NavMesh.AllAreas))
            {
                if (!navAgent.enabled)
                {
                    navAgent.enabled = true;
                    navAgent.SetDestination(transform.position);
                }
            }
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

    }

    protected void OnCollisionExit(Collision collision)
    {
        
    }
}
