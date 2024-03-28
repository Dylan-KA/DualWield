using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : ParticleWeapon
{
    protected override void Start()
    {
        handOffset = new Vector3(.5f, -.35f, .8f);
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
        if (otherWeaponType == WeaponType.Flamethrower)
            FlameAndFlame();
        else if (otherWeaponType == WeaponType.WindGun)
            FlameAndWind();
    }

    private void FlameAndFlame()
    {
        // TODO: implement this
    }
    private void FlameAndWind()
    {
        // TODO: implement this
    }
}
