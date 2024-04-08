using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float explosiveRange;
    [SerializeField] private float explosiveMaxDamage;
    private Rigidbody rigidbody;

    public void Initialize(float speedMult)
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = -transform.forward * baseSpeed * speedMult;
    }

    void Update()
    {
        // move projectile
        //Debug.Log(speed);
        //transform.Translate(0,0, -speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerMask = 0;
        layerMask |= 1 << 3; // only gets enemies
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
        foreach (Collider collider in hitColliders)
        {
            Debug.Log(collider.gameObject.name);
            BaseEnemy enemy = collider.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(explosiveMaxDamage);
            //enemy.TakeDamage();
        }
    }


}
