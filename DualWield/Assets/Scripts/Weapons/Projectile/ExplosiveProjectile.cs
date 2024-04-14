using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosiveRange;

    protected override void OnImpact(Collider _)
    {
        if (!isFiredByEnemy)
        {
            int layerMask = 0;
            layerMask |= 1 << 3; // only gets enemies
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
            foreach (Collider collider in hitColliders)
            {
                BaseEnemy enemy = collider.gameObject.GetComponent<BaseEnemy>();
                float distance =  (transform.position - enemy.transform.position).magnitude - enemy.transform.localScale.magnitude;
                distance = Mathf.Clamp(distance, 0, explosiveRange);
                
                float damagePercent;
                if (distance < explosiveRange/2f)
                    // deal full damage
                    damagePercent = 1;
                    
                else
                    // deal fractional damage
                    damagePercent = ((explosiveRange - distance)/2f) / (explosiveRange/2f);

                enemy.TakeDamage(baseDamage * damagePercent);
                if (gameObject.name == "RocketFreeze(Clone)") 
                    enemy.AddFreezePercent(damagePercent * -5);
            }
        }
    }
}
