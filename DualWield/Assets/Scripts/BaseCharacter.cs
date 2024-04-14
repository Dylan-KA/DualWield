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
    [SerializeField] protected float health = maxHealth;
    [SerializeField] protected StatusEffect statusEffect = StatusEffect.None;
    [SerializeField] private float FreezePercent = 0.0f;
    [SerializeField] protected PhysicMaterial freezeMaterial;
    protected MeshRenderer rend;
    protected Color characterColor;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rend = GetComponent<MeshRenderer>();
        characterColor = rend.material.color;
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

    public virtual void AddFreezePercent(float freezeChange)
    {
        FreezePercent += freezeChange;
        if (FreezePercent >= 100.0f)
        {
            FreezePercent = 100.0f;
            Freeze();
        }
        VisualFreezeEffect(FreezePercent);
    }

    public void Freeze()
    {
        statusEffect = StatusEffect.Freeze;
        this.gameObject.GetComponent<BoxCollider>().material = freezeMaterial;
        if (this.gameObject.GetComponent<BaseEnemy>())
        {
            this.gameObject.GetComponent<BaseEnemy>().SetSquashDamage(999999);
        }
    }

    // frozenPercentage of 0.0 is normal, 1.0 is fully frozen
    private void VisualFreezeEffect(float frozenPercentage)
    {
        float frozenFloat = frozenPercentage / 100.0f;
        Color newColor = new Color(frozenFloat, frozenFloat, frozenFloat);
        rend.material.SetColor("_EmissionColor", newColor);
        Debug.Log("Visual Freeze Effect: " + frozenPercentage + " newColor: " + newColor);
    }

    public void RecoverHP(float recovedAmount)
    {
        health += recovedAmount;
        if (health > 100)
        {
            health = 100;
        }
    }
}
