using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ParticleWeapon : BaseWeapon
{
    [SerializeField] protected BaseEnemy[] ListofEnemies;
    [SerializeField] protected float ammoPerSecond;

    [SerializeField] private ParticleSystem NormalRangeParticles;
    [SerializeField] private ParticleSystem ExtendedRangeParticles;
    [SerializeField] private ParticleSystem FireWindParticles;
    [SerializeField] private ParticleSystem RocketWindParticles;
    [SerializeField] protected ParticleSystem WeaponParticles; // The value is swapped between the 2 ranges above
    private bool particlesPlaying = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        GenerateParticles();

    }

    private void Awake()
    {
        
    }

    public override void Fire()
    {
        base.Fire();
        GameManager.Instance.DrainAmmo(ammoPerSecond * 0.01f * (3600f/roundsPerMinute));
    }


    public override void SetOtherWeaponType(WeaponType otherWeaponType)
    {
        base.SetOtherWeaponType(otherWeaponType);
    }

    private void FixedUpdate()
    {
        ListofEnemies = GetCollidingObjects();
        
    }
    protected BaseEnemy[] GetCollidingObjects()
    {
        Collider[] colliders = Physics.OverlapBox(GetComponent<BoxCollider>().bounds.center,GetComponent<BoxCollider>().bounds.extents, Quaternion.identity);
        
        var enemyColliders = colliders.Where(collider => collider.CompareTag("Enemy")).ToArray();
        
        BaseEnemy[] hitObjects = new BaseEnemy[enemyColliders.Length];
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            hitObjects[i] = enemyColliders[i].gameObject.GetComponent<BaseEnemy>();
        }
        return hitObjects;
    }

    public void GenerateParticles()
    {
        if (WeaponParticles == null) { return; }
        if (isFiring)
        {
            if (!particlesPlaying) //If particles not already playing.
            {
                WeaponParticles.Play();
                particlesPlaying = true;
            }
        }
        else
        {
            WeaponParticles.Stop();
            particlesPlaying = false;
        }
    }

    public void SetParticleRangeNormal()
    {
        if (WeaponParticles) { WeaponParticles.Stop(); } // Clear particles before changing range
        WeaponParticles = NormalRangeParticles;
    }

    public void SetParticleRangeExtended()
    {
        if (WeaponParticles) { WeaponParticles.Stop(); } // Clear particles before changing range
        WeaponParticles = ExtendedRangeParticles;
    }
    public void SetParticleRed()
    {
        if (WeaponParticles) { WeaponParticles.Stop(); } // Clear particles before changing range
        WeaponParticles = FireWindParticles;
    }

    public void SetParticleGreen()
    {
        if (WeaponParticles) { WeaponParticles.Stop(); } // Clear particles before changing range
        WeaponParticles = RocketWindParticles;
    }
}
