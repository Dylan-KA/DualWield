using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class FreezeGun : HitScanWeapon
{
    
    // Start is called before the first frame update
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
                FreezeAndFlame();
                break;
            case WeaponType.FreezeGun:
                FreezeAndFreeze();
                break;
            case WeaponType.RocketLauncher:
                FreezeAndRocket();
                break;
            case WeaponType.WindGun:
                FreezeAndWind();
                break;
        }
        
    }

    private void FreezeAndWind()
    {
        if (GetHitEnemy())
        {
            GetHitEnemy().TemperatureChange(-1);
        }
    }

    private void FreezeAndRocket()
    {
        throw new System.NotImplementedException();
    }

    private void FreezeAndFreeze()
    {
        if (GetHitEnemy())
        {
            GetHitEnemy().TemperatureChange(-1f);
            if (GetHitEnemy().GetStatueEffect() == StatusEffect.Freeze)
            {
                GetHitEnemy().TakeDamage(5f);
            }
        }
    }

    private void FreezeAndFlame()
    {
        if (GetHitEnemy())
        {
            GetHitEnemy().TemperatureChange(-1f);
        }
    }
}
