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
                Enemy.TakeDamage(25 * Time.deltaTime );
            }
         }
         
         
    }
    private void FlameAndWind()
    {
        if(ListofEnemies.Length != 0){
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.TakeDamage(25*Time.deltaTime);
            }
        }
    }

    private void IncreaseFlameSize()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x,GetComponent<BoxCollider>().size.y ,  6);
        GetComponent<BoxCollider>().center =
            new Vector3(GetComponent<BoxCollider>().center.x, GetComponent<BoxCollider>().center.y, 2);
    }

    private void ResetFlameSize()
    {
        GetComponent<BoxCollider>().size = new Vector3(GetComponent<BoxCollider>().size.x,
            GetComponent<BoxCollider>().size.y, 3);
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
