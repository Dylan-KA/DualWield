using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Flamethrower : ParticleWeapon
{
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
                Enemy.TakeDamage(10);
            }
         }
         
         
    }
    private void FlameAndWind()
    {
        IncreaseFlameSize();
        FlameAndFlame();
        StartCoroutine(ResetFlameSize());

    }

    private void IncreaseFlameSize()
    {
        weaponCollider.transform.localScale = new Vector3(weaponCollider.transform.localScale.x, weaponCollider.transform.localScale.y,  10);
    }

    IEnumerator ResetFlameSize()
    {
        yield return new WaitForSeconds(3);
        weaponCollider.transform.localScale = new Vector3(weaponCollider.transform.localScale.x,
            weaponCollider.transform.localScale.y, 6.5f);
    }



}
