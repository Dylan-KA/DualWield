using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public abstract class HitScanWeapon : BaseWeapon
{
    [SerializeField] private float fireRate = 15f;

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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.transform.CompareTag("Enemy") ? "You hit an enemy" : "You hit something that isn't an enemy");
           
        }
        
        return hit.transform.GetComponent<BaseEnemy>();
    }
}


