using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffect
{
    None,
    Freeze,
    Burn,
    Wet
}

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected float movementSpeed;
    protected const float MAX_HEALTH = 100;
    protected float health = MAX_HEALTH;
    protected StatusEffect statusEffect = StatusEffect.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }

}
