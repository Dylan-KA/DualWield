using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ParticleWeapon : BaseWeapon
{
    [SerializeField]private GameObject[] /*BaseEnemy[]*/ ListofEnemies;
    [SerializeField] private BoxCollider weaponCollider;
    
    
    protected override void Start()
    {
        
        base.Start();
        
    }

    protected override void Update()
    {
        base.Update();
        
    }

    private void Awake()
    {
        
    }

    public abstract override void Fire();

    public void EquipCollider()
    {
        if (hand == Hand.Right)
        {
            weaponCollider = transform.parent.GetChild(0).GetComponent<BoxCollider>();
        }
        if (hand == Hand.Left)
        {
            weaponCollider = transform.parent.GetChild(1).GetComponent<BoxCollider>();
        }
    }
    protected GameObject[]/*Change to BaseEnemy[]*/ GetHitEnemies()
    {
        return ListofEnemies;
    }

    private void FixedUpdate()
    {
        if (!weaponCollider)
        {
            EquipCollider();
        }
        
        if (weaponCollider)
        {
            
            ListofEnemies = GetCollidingObjects();
        }
    }
    protected GameObject[] GetCollidingObjects()
    {
        Collider[] colliders = Physics.OverlapBox(weaponCollider.bounds.center, weaponCollider.bounds.extents, Quaternion.identity);
        
        var enemyColliders = colliders.Where(collider => collider.CompareTag("Enemy")).ToArray();
        
        GameObject[] hitObjects = new GameObject[enemyColliders.Length];
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            hitObjects[i] = enemyColliders[i].gameObject;
        }
        return hitObjects;
    }
}
