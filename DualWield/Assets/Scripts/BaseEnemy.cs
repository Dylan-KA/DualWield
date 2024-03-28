using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected GameObject enemyModel;
    protected EnemyTypes enemyType;
    protected float speed = 1f;
    protected float attackRange;
    protected float fieldOfView;

    public LayerMask playerMask = 6;
    public LayerMask obstructionMask = 7;
    private Transform playerTransform;
    private bool isPlayerSeen;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch
        {
            Debug.Log("Player cannot be found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }
    private void MoveTowardsTarget()
    {
        if (playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
    }
}

public enum EnemyTypes
{
    Fodder,
    Tank,
    Ranged,
}
