using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [SerializeField] private float explosiveRange;
    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private GameObject molotovPrefab;

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
                if (!enemy) continue;
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
                    enemy.AddFreezePercent(damagePercent * 50f);
            }
            
            if (molotovPrefab)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out var hitInfo, 4f, LayerMask.GetMask("Environment")))
                {
                    Debug.Log("did u work");
                    Instantiate<GameObject>(molotovPrefab, hitInfo.point + Vector3.up * 0.1f, Quaternion.identity);
                }

            }
            
            ParticleSystem explosion = Instantiate<ParticleSystem>(explosionPrefab, transform.position, new Quaternion());
            Destroy(explosion, 1.9f);
        }
        else
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
                    // deal full damage
                    damagePercent = 1;

                else
                    // deal fractional damage
                    damagePercent = ((explosiveRange - distance) / 2f) / (explosiveRange / 2f);

                player.TakeDamage(baseDamage * damagePercent);
            }
        }
    }
}
