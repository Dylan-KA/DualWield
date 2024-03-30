using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected GameObject enemyModel;
    protected EnemyTypes enemyType;
    [Range(0, 360)]
    [SerializeField] protected float fieldOfView;
    [SerializeField] protected float viewDistance;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    //[SerializeField] protected float rotationSpeed;
    public LayerMask playerMask;
    public LayerMask obstructionMask;
    private Transform playerTransform;
    private bool isPlayerSeen;
    private bool isAttackOnCooldown;
    public float GetFieldOfView()
    {
        return fieldOfView;
    }
    public float GetViewDistance()
    {
        return viewDistance;
    }
    public bool GetIsPlayerSeen()
    {
        return isPlayerSeen;
    }
    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        try
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            StartCoroutine(FOVRoutine());
        }
        catch
        {
            Debug.Log("Player cannot be found");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isPlayerSeen)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
            {
                if (!isAttackOnCooldown)
                {
                    isAttackOnCooldown = true;
                    StartCoroutine(AttackSpeed());
                }
            }
            else
            {
                MoveTowardsTarget();
            }
        }
    }
    private IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewDistance, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    isPlayerSeen = true;
                }
                else
                {
                    isPlayerSeen = false;
                }
            }
            else
            {
                isPlayerSeen = false;
            }
        }
        else if (isPlayerSeen)
        {
            isPlayerSeen = false;
        }
    }
    private void MoveTowardsTarget()
    {
        if (playerTransform != null)
        {
            transform.LookAt(playerTransform);
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
        }
    }
    private IEnumerator AttackSpeed()
    {
        Attack();
        yield return new WaitForSeconds(attackSpeed);
        isAttackOnCooldown = false;
    }
    virtual protected void Attack() 
    {
        Debug.Log(gameObject.name + "attacked the Player at: " + attackRange);
    }

    public override void TakeDamage(float damageAmount)
    {
        if (health <= 0) return;

        base.TakeDamage(damageAmount);
        if (health <= 0)
            Destroy(gameObject);
    }
}
