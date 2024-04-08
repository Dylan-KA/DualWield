using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float explosiveRange;
    [SerializeField] private float explosiveMaxDamage;
    private float speed;

    public void Initialize(float speedMult)
    {
        speed = baseSpeed * speedMult;
    }

    void Update()
    {
        // move projectile
        //Debug.Log(speed);
        transform.Translate(0,0, -speed * Time.deltaTime);
    }

    private OnTriggerEnter(Collider other)
    {
        int layerMask = 1 << 8; // only gets enemies
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosiveRange, layerMask);
        foreach (Collider collider in hitColliders)
        {
            BaseEnemy enemy = collider.GetComponent<BaseEnemy>();
            Debug.Log(collider.contactOffset);
            //enemy.TakeDamage();
        }
    }


}
