using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFlyingEnemy : FlyingEnemy
{
    [SerializeField] protected float diveSpeed = 5;
    [SerializeField] protected float returnSpeed = 10;
    [SerializeField] protected Vector3 velocity = Vector3.zero;
    [SerializeField] protected float attackHitRange = 1;
    [SerializeField] private bool isDiving = false;
    protected override void Attack()
    {
        if (isDiving == false)
        {
            StartCoroutine(AttackAnim());
        }
    }
    protected IEnumerator AttackAnim()
    {
        isDiving = true;
        navAgent.enabled = false;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = playerTransform.position;
        // Diving towards the target
        while (Vector3.Distance(transform.position, targetPosition) >= 0.1f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, diveSpeed * Time.deltaTime);
            yield return null;
        }
        // Returning to initial position
        while (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }
        ResetAttackWaitTime();
        navAgent.enabled = true;
        isDiving = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isAttacking)
            {
                other.gameObject.GetComponent<PlayerCharacter>().TakeDamage(attackDamage);
            }
        }
    }
}
