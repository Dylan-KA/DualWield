using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("TEst2");
        }
            
    }

    private void FlameAndFlame()
    {
        Debug.Log("TEst3");
         /*
          if(ListofEnemies){
            foreach (BaseEnemy Enemy in ListofEnemies)
            {
                Enemy.TakeDamage();
            }
         }
         */
         
    }
    private void FlameAndWind()
    {
        Debug.Log("TEst4");
    }
}
