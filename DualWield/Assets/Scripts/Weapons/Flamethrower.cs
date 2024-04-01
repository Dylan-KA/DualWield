using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Flamethrower : ParticleWeapon
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float flameAndFlameMultiplier;

    protected override void Start()
    {
        
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
       
    }
    
    /// <summary>
    /// Shoots the gun, if possible
    /// </summary>
    public override void Fire()
    {

        if (otherWeaponType == WeaponType.Flamethrower)
        {

            FlameAndFlame();
            
        }
        else if (otherWeaponType == WeaponType.WindGun)
        {
            FlameAndWind();
        }
            
    }

    private void FlameAndFlame()
    {
        if(ListofEnemies.Length != 0){
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.TakeDamage(baseDamage * flameAndFlameMultiplier * Time.deltaTime);
            }
        }
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
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x,GetComponent<BoxCollider>().size.y ,  8);
        GetComponent<BoxCollider>().center =
            new Vector3(GetComponent<BoxCollider>().center.x, GetComponent<BoxCollider>().center.y, 3);
    }

    private void ResetFlameSize()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x,
            GetComponent<BoxCollider>().size.y, 5);
        GetComponent<BoxCollider>().center = new Vector3(GetComponent<BoxCollider>().center.x, GetComponent<BoxCollider>().center.y, 2);
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

}
