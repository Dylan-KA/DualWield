using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BaseEnemy : BaseCharacter
{
    public LayerMask playerMask;
    public LayerMask obstructionMask;

    private bool isFlickering = false;
    private bool isPlayerSeen = false;
    private float dmgFlickerRate = 0.15f;

    protected GameObject enemyModel;
    protected NavMeshAgent navAgent;
    protected Transform playerTransform;
    protected EnemyTypes enemyType;
    protected bool isAttacking = false;
    protected float currentMovementSpeed;
    protected float currentAttackTimer = 0f;
    protected float fieldOfView = 350;

    [SerializeField] protected float viewDistance;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange = 1;
    [SerializeField] protected float attackWaitTime = 1;
    [SerializeField] protected float squashDamage = 10;
    [SerializeField] protected float squashThreshHold = 5;
    [SerializeField] protected bool isAlwaysHuntingTarget = false;

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

    protected virtual void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        SetEnemyVision();
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        try
        {
            navAgent = GetComponent<NavMeshAgent>();
            SetEnemyVision();
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
        if (currentAttackTimer >= 0)
        {
            currentAttackTimer -= Time.deltaTime;
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void Freeze()
    {
        base.Freeze();
        if (navAgent)
        {
            StopEnemyMovement();
        }
    }

    // Implemented in ground/flying enemy script
    protected virtual void MoveTowardsTarget() { }
    // Implemented in the individual enemy script
    protected virtual void Attack() { }

    private void SetEnemyVision()
    {
        StartCoroutine(FOVRoutine());
    }

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

    protected virtual void EnemyAttackAtCertainRange()
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
                if (currentAttackTimer <= 0)
                {
                    Attack();
                }
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
        return Vector3.Distance(transform.position, playerTransform.position) <= attackRange;
    }

    protected virtual void LookAtPlayer()
    {
        transform.LookAt(playerTransform);
    }

    // Sets the movement speed based on how frzozen the enemy is
    protected virtual void SetMovementSpeed()
    {
        navAgent.speed = baseMovementSpeed * (1 - (FreezePercent / 100));
    }

    public override void TakeDamage(float damageAmount)
    {
        if (health <= 0) return;

        base.TakeDamage(damageAmount);
        if (health <= 0)
            PlayAudio();
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
    protected void StopEnemyMovement()
    {
        navAgent.enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
    
    // Audio Management //
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] creature;

    void PlayAudio()
    {
        var i = Random.Range(0, creature.Length);
        audioSource.PlayOneShot(creature[i]);
    }
    
    //                  //
}
