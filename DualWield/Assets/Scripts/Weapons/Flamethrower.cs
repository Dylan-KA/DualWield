using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.PlayerLoop;

public class Flamethrower : ParticleWeapon
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float damageMultiplier = 1.0f;
    private float maxMultiplier = 7.0f;
    private float muliplierIncreaseRate = 30.0f;

    [SerializeField] private List<Color> defaultParticleColors;
    [SerializeField] private List<Color> BlueFlameColors;

    protected override void Start()
    {
        
        base.Start();
        damageMultiplier = 1.0f;
    }

    protected override void Update()
    {
        base.Update();
        if (otherWeaponType == WeaponType.Flamethrower)
        {
            if (!isFiring)
            {
                damageMultiplier = 1.0f;
                ResetFlame();
            }
        }
    }
    
    /// <summary>
    /// Shoots the gun, if possible
    /// </summary>
    public override void Fire()
    {
        base.Fire();

        switch (otherWeaponType)
        {
            case WeaponType.Flamethrower:
                FlameAndFlame();
                break;
            case WeaponType.FreezeGun:
                FlameAndFreeze();
                break;
            case WeaponType.RocketLauncher:
                FlameAndRocket();
                break;
            case WeaponType.WindGun:
                FlameAndWind();
                break;
        }
            
    }

    private void FlameAndFreeze()
    {
        if(ListofEnemies.Length != 0){
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.TakeDamage(baseDamage * Time.deltaTime);
            }
        }
    }

    private void FlameAndRocket()
    {
        
    }
    private void FlameAndFlame()
    {
        if(ListofEnemies.Length != 0){
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.TakeDamage(baseDamage * damageMultiplier * Time.deltaTime);
            }
        }
        IncreaseMultiplier();
        BlueDamageFlame();
    }
    private void FlameAndWind()
    {
        if(ListofEnemies.Length != 0){
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.TakeDamage(baseDamage * Time.deltaTime);
            }
        }
    }

    private void IncreaseFlameSize()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x,GetComponent<BoxCollider>().size.y, 20);
        GetComponent<BoxCollider>().center =
            new Vector3(GetComponent<BoxCollider>().center.x, GetComponent<BoxCollider>().center.y, 8);
        base.SetParticleRangeExtended();
    }

    private void ResetFlameSize()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x,
            GetComponent<BoxCollider>().size.y, 5);
        GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, GetComponent<BoxCollider>().center.y, 2);
        base.SetParticleRangeNormal();
    }

    public override void SetOtherWeaponType(WeaponType otherWeaponType)
    {
        base.SetOtherWeaponType(otherWeaponType);
        if (otherWeaponType == WeaponType.WindGun)
        {
            IncreaseFlameSize();
        }
        else
        {
            ResetFlameSize();
        }
    }

    private void IncreaseMultiplier()
    {
        if (damageMultiplier >= maxMultiplier) { damageMultiplier = maxMultiplier; }
        damageMultiplier += (muliplierIncreaseRate * Time.deltaTime);
    }

    public void RestMultiplier() { damageMultiplier = 1.0f; }

    
    private void BlueDamageFlame()
    {
        if (defaultParticleColors.Count == 0) { PopulateDefaultColours(); }
        float clampedMultiplier = (damageMultiplier / 5);
        if (clampedMultiplier > 0.5f) { clampedMultiplier = 0.8f; }
        if (clampedMultiplier > 1.0f) { clampedMultiplier = 1.0f; }
        Color parentLerpCol = Color.Lerp(defaultParticleColors[0], BlueFlameColors[0], clampedMultiplier);
        WeaponParticles.startColor = parentLerpCol;
        ParticleSystem[] ChildParticles = WeaponParticles.GetComponentsInChildren<ParticleSystem>();
        for (int i = 1; i < ChildParticles.Length; i++)
        {
            Color lerpedColor = Color.Lerp(defaultParticleColors[i], BlueFlameColors[i], clampedMultiplier);
            ChildParticles[i].startColor = lerpedColor;
        }
    }

    private void ResetFlame()
    {
        if (defaultParticleColors.Count == 0) { PopulateDefaultColours(); }
        WeaponParticles.startColor = defaultParticleColors[0];
        ParticleSystem[] ChildParticles = WeaponParticles.GetComponentsInChildren<ParticleSystem>();
        for (int i = 1; i < ChildParticles.Length; i++)
        {
            ChildParticles[i].startColor = defaultParticleColors[i];
        }
    }

    private void PopulateDefaultColours()
    {
        //defaultParticleColors.Add(WeaponParticles.startColor);
        ParticleSystem[] ChildParticles = WeaponParticles.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particle in ChildParticles) { defaultParticleColors.Add(particle.startColor); }
    }
    
}
