using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : BaseEnemy
{
    protected NavMeshAgent navAgent;
    private bool isPushed = false;

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

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.relativeVelocity.magnitude > squashThreshHold && collision.gameObject.CompareTag("Untagged") && isPushed)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            TakeDamage(squashDamage);
            isPushed = false;
        }
    }

    protected override void EnemyAttackAtCertainRange()
    {
        if (statusEffect != StatusEffect.Freeze)
        {
            if (isAlwaysHuntingTarget)
            {
                ArenaBehaviour();
            }
            else
            {
                NormalBehaviour();
            }
        }
    }

    protected void NormalBehaviour()
    {
        if (GetIsPlayerSeen())
        {
            if (IsPlayerInAttackingRange())
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

    protected void ArenaBehaviour()
    {
        if (GetIsPlayerSeen() && IsPlayerInAttackingRange())
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

    private bool IsPlayerInAttackingRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= attackRange || 
            (isAttacking && Vector3.Distance(transform.position, playerTransform.position) <= attackRange + extendedAttackRange);
    }

    protected override void LookAtPlayer()
    {
        transform.LookAt(playerTransform);
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = 0f;
        transform.eulerAngles = currentRotation;
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
        isPushed = true;
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
        isPushed = false;
    }
}
