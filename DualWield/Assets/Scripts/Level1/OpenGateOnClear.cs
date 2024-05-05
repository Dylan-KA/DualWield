using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateOnClear : MonoBehaviour
{
    [SerializeField] private Transform enemies;
    [SerializeField] private GameObject door;

    // Update is called once per frame
    void Update()
    {
        EnemyCheck();
    }

    void EnemyCheck()
    {
        if (enemies.childCount == 0)
        {
            door.SetActive(false);
        }
    }
}
