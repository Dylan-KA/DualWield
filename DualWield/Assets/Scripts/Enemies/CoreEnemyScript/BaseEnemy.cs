using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : BaseCharacter
{
    public LayerMask playerMask;
    public LayerMask obstructionMask;

    private bool isFlickering = false;
    private bool isPlayerSeen = false;
    private float dmgFlickerRate = 0.15f;

    protected float currentMovementSpeed;
    protected GameObject enemyModel;
    protected Transform playerTransform;
    protected EnemyTypes enemyType;
    protected bool isAttacking = false;
    protected float currentAttackTimer = 0f;
    protected float fieldOfView = 350;

    [SerializeField] protected float viewDistance;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange = 1;
    [SerializeField] protected float attackWaitTime = 1;
    [SerializeField] protected float squashDamage = 10;
    [SerializeField] protected float squashThreshHold = 5;

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
    protected override void Start()
    {
        base.Start();
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

    private void OnEnable()
    {
        StartCoroutine(FOVRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        EnemyAttackAtCertainRange();
        if (currentAttackTimer >= 0)
        {
            currentAttackTimer -= Time.deltaTime;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    // Implemented in enemies ground/flying
    protected virtual void EnemyAttackAtCertainRange() { }
    protected virtual void MoveTowardsTarget() { }
    // Implemented in the individual enemy script
    protected virtual void Attack() { }

    private IEnumerator FOVRoutine()
    {
        float delay = 2f;
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

    protected virtual void LookAtPlayer()
    {
        transform.LookAt(playerTransform);
    }

    public override void TakeDamage(float damageAmount)
    {
        if (health <= 0) return;

        base.TakeDamage(damageAmount);
        if (health <= 0)
            Destroy(gameObject);

        if (!isFlickering && statusEffect != StatusEffect.Freeze)
        {
            RedDamageFlicker();
            isFlickering = true;
        }
    }

    private void RedDamageFlicker()
    {
        Color newColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        rend.material.SetColor("_Color", newColor);
        Invoke(nameof(ResetDamageFlicker), dmgFlickerRate);
    }

    private void ResetDamageFlicker()
    {
        rend.material.SetColor("_Color", characterColor);
        Invoke(nameof(ReadyForNextFlicker), dmgFlickerRate);
    }

    private void ReadyForNextFlicker()
    {
        isFlickering = false;
    }

    public void SetSquashDamage(float newDamge)
    {
        squashDamage = newDamge;
    }

    protected void DamagePlayer()
    {
        playerTransform.gameObject.GetComponentInParent<BaseCharacter>().TakeDamage(attackDamage);
    }

    protected bool IsAttackingValid()
    {
        return isAttacking && currentAttackTimer >= attackWaitTime;
    }

    protected void ResetAttack()
    {
        isAttacking = false;
    }

    protected void ResetAttackWaitTime()
    {
        currentAttackTimer = attackWaitTime;
    }
}
