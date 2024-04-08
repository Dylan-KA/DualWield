using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : BaseEnemy
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
        Instantiate(projectile, projectileSpawnTransform);
    }
}
