using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : Projectile
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnImpact(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("Enemy"))
        {
            collider.GetComponent<BaseCharacter>().TakeDamage(baseDamage);
        }
    }
}
