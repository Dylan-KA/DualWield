using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderEnemy : BaseEnemy
{
    new const float maxHealth = 20;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = maxHealth;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack()
    {
        //Debug.Log("Fodder Attacking");
    }
}
