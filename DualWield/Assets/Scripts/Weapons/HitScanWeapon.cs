using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public abstract class HitScanWeapon : BaseWeapon
{
    [SerializeField] private LineRenderer lineRend;
    private float _nextFireTime;

    public override void Fire()
    {
        base.Fire();
    }

    public BaseEnemy GetHitEnemy()
    {
        RaycastHit hit;
        
        lineRend.enabled = true;
        
        if (Physics.Raycast(transform.parent.position, transform.TransformDirection(Vector3.forward), out hit, 500f, LayerMask.GetMask("Enemy")))
        {
            Debug.Log("Hit: " + hit);
            if (hit.transform.CompareTag("Enemy"))
            {
                return hit.transform.GetComponent<BaseEnemy>();
            }
        }
      
        return null;
    }
}


