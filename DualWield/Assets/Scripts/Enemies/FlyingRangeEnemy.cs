using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRangeEnemy : FlyingEnemy
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private GameObject projectile;

    protected override void Attack()
    {
        Instantiate(projectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
    }
}
