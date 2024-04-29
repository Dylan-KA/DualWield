using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : GroundEnemy
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
        // Rotate the projectile spawn based on player position
        AimAtPlayer();
        GameObject bullet = Instantiate(projectile, projectileSpawnTransform.position, projectileSpawnTransform.rotation);
        ResetAttackWaitTime();
    }

    private void AimAtPlayer()
    {
        projectileSpawnTransform.LookAt(playerTransform);
    }
}
