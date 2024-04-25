using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FodderEnemy : GroundEnemy
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
        DamagePlayer();
        ResetAttackWaitTime();
    }
}
