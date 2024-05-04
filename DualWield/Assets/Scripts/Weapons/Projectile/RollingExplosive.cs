using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RollingExplosive : Projectile
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private Vector3 ThrowVector;
    [SerializeField] private float explosionTimer = 2.0f;
    [SerializeField] private float explosiveRange;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        ThrowBomb();
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    protected override void OnImpact(Collider _)
    {
        
    }

    protected override void ProjectileMovement() { }

    //Throw bomb towards the player
    public void ThrowBomb()
    {
        Debug.Log("Bomb Thrown");
        rb.AddForce(ThrowVector, ForceMode.Impulse);
        Invoke(nameof(Explode), explosionTimer);
    }

    // Deal damage to any players/enemies in the range of the bomb
    private void Explode()
    {
        Debug.Log("Bomb Exploded");

        int layerMask = 0;
        layerMask |= 1 << 6;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
        foreach (Collider collider in hitColliders)
        {
            PlayerCharacter player = collider.gameObject.GetComponent<PlayerCharacter>();
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

        explosionParticles.Play();
        Invoke(nameof(DestroySelf), 1.0f);
    }

    private void DestroySelf()
    {
        Debug.Log("Hiding Bomb Mesh");
        meshRenderer.enabled = false;
        //Destroy(gameObject);
    }
}
