using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : BaseEnemy
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private Transform bombSpawnTransform;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }

    // Throws explosive bombs at player
    protected override void Attack()
    {
        Debug.Log("Spawn Bomb");
        GameObject newBomb = Instantiate(bomb, bombSpawnTransform.position, bombSpawnTransform.rotation);
        bomb.GetComponent<TankBomb>().SetExplosionDamage(attackDamage);
        bomb.GetComponent<TankBomb>().ThrowBomb();
    }
}
