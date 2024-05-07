using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : GroundEnemy
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();   
    }
    protected override void Attack()
    {
        AimAtPlayer();
        Instantiate(projectilePrefab, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        ResetAttackWaitTime();
    }
    private void AimAtPlayer()
    {
        projectileSpawnTransform.LookAt(playerTransform);
    }
}
