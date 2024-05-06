using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class HitScanWeapon : BaseWeapon
{
    [SerializeField] protected float ammoPerSecond;
    [SerializeField] protected LineRenderer lineRend;
    private float _nextFireTime;

    public override void Fire()
    {
        base.Fire();
        GameManager.Instance.DrainAmmo(ammoPerSecond * 0.01f * (3600f / roundsPerMinute));
    }

    public BaseEnemy GetHitEnemy()
    {
        RaycastHit hit;
        
        lineRend.enabled = true;
        
        if (Physics.Raycast(transform.parent.position, transform.TransformDirection(Vector3.forward), out hit, 500f, LayerMask.GetMask("Enemy", "Environment")))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                return hit.transform.GetComponent<BaseEnemy>();
            }
        }
      
        return null;
    }
}


