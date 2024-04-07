using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public abstract class HitScanWeapon : BaseWeapon
{
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private LineRenderer lineRend;
    private float _nextFireTime;

    
    public bool IsCoolingDown()
    {
        return Time.time < _nextFireTime;
    }

    public void StartCooldown()
    {
        _nextFireTime = Time.time + 1f/ fireRate;
    }
    public BaseEnemy GetHitEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity ))
        {
            lineRend.enabled = true;
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, hit.point);
            if (hit.transform.CompareTag("Enemy"))
            {
                
                return hit.transform.GetComponent<BaseEnemy>();
            }
            
        }
        else
        {
            lineRend.enabled = false;
        }
        
        return null;
    }
}


