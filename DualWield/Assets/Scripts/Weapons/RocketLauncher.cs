using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RocketLauncher : ProjectileWeapon
{
    [SerializeField] private ExplosiveProjectile rocketRocketPrefab;
    [SerializeField] private ExplosiveProjectile rocketWindPrefab;
    [SerializeField] private ExplosiveProjectile rocketFlamePrefab;
    [SerializeField] private ExplosiveProjectile rocketFreezePrefab;


    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Fire()
    {
        base.Fire();

        switch (otherWeaponType)
        {
            case WeaponType.Flamethrower:
                RocketAndFlame();
                break;
            case WeaponType.FreezeGun:
                RocketAndFreeze();
                break;
            case WeaponType.RocketLauncher:
                RocketAndRocket();
                break;
            case WeaponType.WindGun:
                RocketAndWind();
                break;
        }
    }

    private void RocketAndRocket()
    {
        Projectile projectile = Instantiate(rocketRocketPrefab, transform.position, transform.rotation);
    }

    private void RocketAndWind()
    {
        Projectile projectile = Instantiate(rocketWindPrefab, transform.position, transform.rotation);
    }

    private void RocketAndFlame()
    {
        Projectile projectile = Instantiate(rocketFlamePrefab, transform.position, transform.rotation);
    }

    private void RocketAndFreeze()
    {
        Projectile projectile = Instantiate(rocketFreezePrefab, transform.position, transform.rotation);
    }
}
