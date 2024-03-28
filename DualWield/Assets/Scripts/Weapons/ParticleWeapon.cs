using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleWeapon : BaseWeapon
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public abstract override void Fire();

    // TODO: implement this: protected BaseEnemy[] GetHitEnemies()
}
