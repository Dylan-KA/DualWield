using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    [SerializeField] protected float ammoPerShot;

    protected override void Update()
    {
        base.Update();
    }

    public override void Fire()
    {
        base.Fire();
        GameManager.Instance.DrainAmmo(ammoPerShot);
    }

    protected override void Start()
    {
        base.Start();
    }

}
