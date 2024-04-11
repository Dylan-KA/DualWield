using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankBomb : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private SphereCollider explosionRadius;
    [SerializeField] private Vector3 ThrowVector;
    [SerializeField] private float explosionDamage = 20.0f;
    public void SetExplosionDamage(float damage) { explosionDamage = damage; }
    [SerializeField] private float explosionTimer = 2.0f;

    [SerializeField] protected List<BaseCharacter> InDamageRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThrowBomb()
    {
        Debug.Log("Bomb Thrown");
        rigidBody.AddForce(ThrowVector, ForceMode.Impulse);
        Invoke("Explode", explosionTimer);
    }

    // When a Player/Enemy enters the collider of the bomb's explosion radius
    // They are ADDED TO the list
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            InDamageRange.Append(other.gameObject.GetComponent<BaseCharacter>());
        }
    }

    // When a Player/Enemy exits the collider of the bomb's explosion radius
    // They are REMOVED FROM the list 
    private void OnTriggerExit(Collider other)
    {
        if (InDamageRange.Contains(other.gameObject.GetComponent<BaseCharacter>()))
        {
            InDamageRange.Remove(other.gameObject.GetComponent<BaseCharacter>());
        }
    }

    // Deal damage to any players/enemies in the range of the bomb
    private void Explode()
    {
        Debug.Log("Bomb Exploded");
        foreach (BaseCharacter character in InDamageRange)
        {
            Debug.Log("Character damaged by tank's bomb: " + character.tag);
            character.TakeDamage(explosionDamage);
        }
        explosionParticles.Play();
        Invoke("DestroySelf", 1.0f);
    }

    private void DestroySelf()
    {
        Debug.Log("Hiding Bomb Mesh");
        meshRenderer.enabled = false;

        //Destroy(gameObject);

    }


}
