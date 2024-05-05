using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform to;
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!to)
        {
            Debug.LogError("Assign a teleport location to this teleporter: " + gameObject.name, this);
        }

        if (!player)
        {
            Debug.LogError("Assign a player to this teleporter: " + gameObject.name, this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        player.transform.position = to.position;
    }
}
