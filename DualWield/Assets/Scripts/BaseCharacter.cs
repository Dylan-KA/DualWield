using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
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
    [SerializeField]protected float health = maxHealth;
    [SerializeField]protected StatusEffect statusEffect = StatusEffect.None;
    [SerializeField] private float Temperature;

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

    public virtual StatusEffect GetStatueEffect()
    {
        return statusEffect;
    }

    public virtual void TemperatureChange(float tempChange)
    {
        Temperature += tempChange;
        Debug.Log(Temperature);
        if (Temperature > -10f)
        {
            movementSpeed += Temperature;
        }
        if (movementSpeed <= 0)
        {
            movementSpeed = 0;
            Freeze();
        }
    }

    public void Freeze()
    {
        statusEffect = StatusEffect.Freeze;
    }
}
