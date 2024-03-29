using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGun : ParticleWeapon
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
    
    /// <summary>
    /// Shoots the gun, if possible
    /// </summary>
    public override void Fire()
    {
        if (otherWeaponType == WeaponType.WindGun)
        {
            
            WindAndWind();
        }
            
        else if (otherWeaponType == WeaponType.Flamethrower)
        {
            
            WindAndFlame();
        }
            
    }

    private void WindAndWind()
    {
        // TODO: implement this
    }
    private void WindAndFlame()
    {
        // TODO: implement this
    }
}
