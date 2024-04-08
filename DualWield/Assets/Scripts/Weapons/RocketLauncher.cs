using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RocketLauncher : ProjectileWeapon
{
    [SerializeField] protected float explosiveRange;
    [SerializeField] protected float explosiveMaxDamage;

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
        base.Fire(explosiveRange, explosiveMaxDamage);

        /*switch (otherWeaponType)
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
        }*/
    }

    private void RocketAndRocket()
    {

    }

    private void RocketAndWind()
    {

    }

    private void RocketAndFlame()
    {

    }

    private void RocketAndFreeze()
    {

    }
}
