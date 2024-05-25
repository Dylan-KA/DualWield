using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollingExplosive : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float baseDamage;
    [SerializeField] private float explosionTimer = 2.0f;
    [SerializeField] private float explosiveRange;
    private bool isExploded = false;
    private bool isFuseActive = false;
    private GameObject explosion;


    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
        else if (!isFuseActive && !collision.gameObject.CompareTag("Enemy"))
        {
            ActivateFuse();
        }
    }

    //Throw bomb towards the player
    public void ActivateFuse()
    {
        isFuseActive = true;
        Invoke(nameof(Explode), explosionTimer);
    }

    // Deal damage to any players/enemies in the range of the bomb
    private void Explode()
    {
        if (isExploded) return;
        isExploded = true;
        int layerMask = 0;
        layerMask |= 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
        foreach (Collider collider in hitColliders)
        {
            PlayerCharacter player = collider.gameObject.GetComponentInParent<PlayerCharacter>();
            float distance = (transform.position - player.transform.position).magnitude - player.transform.localScale.magnitude;
            distance = Mathf.Clamp(distance, 0, explosiveRange);

            float damagePercent;
            if (distance < explosiveRange / 2f)
                // deal full damage
                damagePercent = 1;

            else
                // deal fractional damage
                damagePercent = ((explosiveRange - distance) / 2f) / (explosiveRange / 2f);
            player.TakeDamage(baseDamage * damagePercent);
        }

        EnableParticle();
    }

    private void EnableParticle()
    {
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
        gameObject.GetComponent<SphereCollider>().enabled = false;
        explosion = Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
        Invoke(nameof(DestroySelf), 0.5f);
    }

    private void DestroySelf()
    {
        Destroy(explosion);
        Destroy(gameObject);
    }
}
