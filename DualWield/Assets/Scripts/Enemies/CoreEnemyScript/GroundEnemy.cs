using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : BaseEnemy
{
    private bool isPushed = false;

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

        if (collision.relativeVelocity.magnitude > squashThreshHold && collision.gameObject.layer == 7 && isPushed)
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            TakeDamage(squashDamage);
            isPushed = false;
        }
    }

    protected override void LookAtPlayer()
    {
        transform.LookAt(playerTransform);
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.x = 0f;
        transform.eulerAngles = currentRotation;
    }

    public void PushEnemy()
    {
        navAgent.enabled = false;
        isPushed = true;
        StopCoroutine(ResetNavMesh());
    }

    protected override void MoveTowardsTarget()
    {
        if (navAgent.enabled == true)
        {
            SetMovementSpeed();
            navAgent.SetDestination(playerTransform.position);
        }
        else
        {
            StartCoroutine(ResetNavMesh());
        }
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
