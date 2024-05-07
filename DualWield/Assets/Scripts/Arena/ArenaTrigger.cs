using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private WaveManager arena;

    public void Start()
    {
        if (arena == null) Debug.LogError("Arena is not assigned");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arena.StartWave();
            Destroy(this.gameObject);
        }
    }
}
