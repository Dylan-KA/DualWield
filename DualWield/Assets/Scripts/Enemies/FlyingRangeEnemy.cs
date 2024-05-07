using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemy : FlyingEnemy
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private GameObject projectilePrefab;

    protected override void Attack()
    {
        base.Attack();
        Debug.Log("Attack");
        Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        ResetAttackWaitTime();
    }
}
