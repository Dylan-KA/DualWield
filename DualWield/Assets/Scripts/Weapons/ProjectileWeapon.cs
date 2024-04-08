using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [SerializeField] protected float ammoPerShot;
    [SerializeField] private Projectile projectilePrefab;

    protected override void Update()
    {
        base.Update();
    }

    public override void Fire()
    {
        base.Fire();
        GameManager.Instance.DrainAmmo(ammoPerShot);
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.Initialize(1);
    }

    protected override void Start()
    {
        base.Start();
    }

}
