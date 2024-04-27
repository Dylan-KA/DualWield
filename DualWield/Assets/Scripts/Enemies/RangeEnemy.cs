using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : GroundEnemy
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private GameObject projectile;

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
        GameObject bullet = Instantiate(projectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        ResetAttackWaitTime();
    }
}
