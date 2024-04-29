using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class FreezeGun : HitScanWeapon
{
    [SerializeField] protected float ammoPerSecond;

    // Start is called before the first frame update
    protected override void Start()
    {
        
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        //First frame of mouse release
        if (Input.GetKeyUp(KeyCode.Mouse0)) { lineRend.enabled = false; }
    }


    public override void Fire()
    {
        base.Fire();
        GameManager.Instance.DrainAmmo(ammoPerSecond * Time.deltaTime);
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
            GetHitEnemy().AddFreezePercent(2.5f);
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
            GetHitEnemy().AddFreezePercent(20f);
            if (GetHitEnemy().GetStatueEffect() == StatusEffect.Freeze)
            {
                GetHitEnemy().TakeDamage(20f);
            }
        }
    }

    private void FreezeAndFlame()
    {
        if (GetHitEnemy())
        {
            GetHitEnemy().AddFreezePercent(1.0f);
        }
    }
}
