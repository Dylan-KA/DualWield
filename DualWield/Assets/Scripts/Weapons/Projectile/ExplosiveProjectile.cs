using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosiveRange;
    [SerializeField] private float maxDamage;

    protected override void OnImpact(Collider _)
    {
        if (!isFiredByEnemy)
        {
            int layerMask = 0;
            layerMask |= 1 << 3; // only gets enemies
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
            foreach (Collider collider in hitColliders)
            {
                Debug.Log(collider.gameObject.name);
                BaseEnemy enemy = collider.gameObject.GetComponent<BaseEnemy>();
                enemy.TakeDamage(maxDamage);
                //enemy.TakeDamage();
            }
        }
    }
}
