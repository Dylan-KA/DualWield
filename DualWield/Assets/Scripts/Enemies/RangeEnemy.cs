using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : GroundEnemy
{
    [SerializeField] private Transform projectileSpawnTransform;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float speedMult;

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
        bullet.GetComponent<BulletProjectile>().SetProjectileProperties(attackDamage, projectileSpeed);
    }
}
