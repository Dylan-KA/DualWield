using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ParticleWeapon : BaseWeapon
{
    [SerializeField] protected BaseEnemy[] ListofEnemies;
    [SerializeField] protected BoxCollider weaponCollider;

    private ParticleSystem Particles;
    private bool particlesPlaying = false;

    protected override void Start()
    {
        
        base.Start();
        Particles = GetComponentInChildren<ParticleSystem>();
    }

    protected override void Update()
    {
        base.Update();
        WeaponParticles();
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
    protected BaseEnemy[] GetCollidingObjects()
    {
        Collider[] colliders = Physics.OverlapBox(weaponCollider.bounds.center, weaponCollider.bounds.extents, Quaternion.identity);
        
        var enemyColliders = colliders.Where(collider => collider.CompareTag("Enemy")).ToArray();
        
        BaseEnemy[] hitObjects = new BaseEnemy[enemyColliders.Length];
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            hitObjects[i] = enemyColliders[i].gameObject.GetComponent<BaseEnemy>();
        }
        return hitObjects;
    }

    public void WeaponParticles()
    {
        if (Particles == null) { return; }
        if (isFiring)
        {
            if (!particlesPlaying) //If particles not already playing.
            {
                Particles.Play();
                Debug.Log("Playing Particles");
                particlesPlaying = true;
            }
        }
        else
        {
            Particles.Stop();
            particlesPlaying = false;
            Debug.Log("NO Particles");
        }
    }
}
