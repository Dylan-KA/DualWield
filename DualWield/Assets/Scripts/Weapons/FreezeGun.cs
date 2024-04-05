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
        throw new System.NotImplementedException();
    }

    private void FreezeAndRocket()
    {
        throw new System.NotImplementedException();
    }

    private void FreezeAndFreeze()
    {
        if (IsCoolingDown()) return;
        if (GetHitEnemy())
        {
            GetHitEnemy().TemperatureChange(-2);
            
        }
        StartCooldown();
        
    }

    private void FreezeAndFlame()
    {
        throw new System.NotImplementedException();
    }
}