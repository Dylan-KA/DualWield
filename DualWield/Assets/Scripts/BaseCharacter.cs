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
    [SerializeField] protected const float maxHealth = 100;
    [SerializeField ]protected float health = maxHealth;
    protected StatusEffect statusEffect = StatusEffect.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        
    }
    public virtual void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }
}
