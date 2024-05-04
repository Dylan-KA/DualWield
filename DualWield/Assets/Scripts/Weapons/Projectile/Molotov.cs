using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            enemy.TakeDamage(20 * Time.deltaTime);
        }
    }

}
