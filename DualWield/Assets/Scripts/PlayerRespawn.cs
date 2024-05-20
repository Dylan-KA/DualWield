using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private CheckpointData checkpointData;

    // Start is called before the first frame update
    void Start()
    {
        checkpointData = GameObject.FindGameObjectWithTag("Checkpoint Data").GetComponent<CheckpointData>();
        if (checkpointData.IsPlayerRespawnAtCheckpoint())
        {
            transform.position = checkpointData.GetPosition();
        }
    }
}
