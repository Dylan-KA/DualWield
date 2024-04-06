using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderEnemy : BaseEnemy
{
    //Health is changed in the Fodder's Prefab

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Player") && IsAttackingValid())
        {
            isAttacking = true;
            DamagePlayer();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        isAttacking = false;
    }
}
