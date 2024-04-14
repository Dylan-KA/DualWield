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
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange = 1;
    [SerializeField] protected float extendedAttackRange = 1;
    [SerializeField] protected float attackWaitTime = 1;
    [SerializeField] protected float squashDamage = 10;
    [SerializeField] protected float squashThreshHold = 2;
    //[SerializeField] protected float rotationSpeed;
    public LayerMask playerMask;
    public LayerMask obstructionMask;
    private Transform playerTransform;
    private bool isPlayerSeen = false;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected float currentAttackTimer = 0f;
    private bool isFlickering = false;
    private float dmgFlickerRate = 0.15f;

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

    // Update is called once per frame
    protected virtual void Update()
    {
        EnemyAttackAtCertainRange();
        if (isAttacking && currentAttackTimer < attackWaitTime)
        {
            currentAttackTimer += Time.deltaTime;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.relativeVelocity.magnitude > squashThreshHold && collision.gameObject.CompareTag("Untagged"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            TakeDamage(squashDamage);   
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
    protected virtual void EnemyAttackAtCertainRange()
    {
        if (isPlayerSeen && statusEffect != StatusEffect.Freeze)
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
    private void MoveTowardsTarget()
    {
        if (playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
        }
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
    protected bool IsAttackingValid()
    {
        return isAttacking && currentAttackTimer >= attackWaitTime;
    }
    protected virtual void LookAtPlayer()
    {
        transform.LookAt(playerTransform);
    }
    protected virtual void Attack()
    {
        DamagePlayer();
    }
    protected void DamagePlayer()
    {
        playerTransform.gameObject.GetComponentInParent<BaseCharacter>().TakeDamage(attackDamage);
    }
    protected void ResetAttackWaitTime()
    {
        currentAttackTimer = 0;
    }
    protected void ResetAttack()
    {
        isAttacking = false;
        currentAttackTimer = 0f;
    }
    private void RedDamageFlicker()
    {
        Color newColor = new Color(1.0f, 0.0f, 0.0f);
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
}
