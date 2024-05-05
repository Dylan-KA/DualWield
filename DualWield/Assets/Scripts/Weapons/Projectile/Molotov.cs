using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    [SerializeField] private float damagePerSecond;

    private void Start()
    {
        Destroy(gameObject, 8f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }

}
