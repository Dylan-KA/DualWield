using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    [SerializeField] private float explosiveRange = 5;
    // [SerializeField] private float arcHeight = 1;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void ProjectileMovement() { }

    protected override void OnImpact(Collider _)
    {
        if (isFiredByEnemy)
        {
            int layerMask = 0;
            layerMask |= 1 << 6;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
            foreach (Collider collider in hitColliders)
            {
                PlayerCharacter player = collider.gameObject.GetComponent<PlayerCharacter>();
                float distance = (transform.position - player.transform.position).magnitude - player.transform.localScale.magnitude;
                distance = Mathf.Clamp(distance, 0, explosiveRange);
                float damagePercent;
                if (distance < explosiveRange / 2f)
                {
                    // deal full damage
                    damagePercent = 1;
                }
                else
                {
                    // deal fractional damage
                    damagePercent = ((explosiveRange - distance) / 2f) / (explosiveRange / 2f);
                }
                player.TakeDamage(baseDamage * damagePercent);
            }
        }
    }
}
