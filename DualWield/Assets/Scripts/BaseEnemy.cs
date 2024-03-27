using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : BaseCharacter
{
    protected GameObject enemyModel;
    protected EnemyTypes enemyType;
    protected float attackType;
    protected float fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum EnemyTypes
{
    Fodder,
    Tank,
    Ranged,
}
