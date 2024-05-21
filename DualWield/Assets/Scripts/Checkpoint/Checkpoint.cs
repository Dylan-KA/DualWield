using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointData checkPointData;

    // Start is called before the first frame update
    void Start()
    {
        checkPointData = GameObject.FindGameObjectWithTag("Checkpoint Data").GetComponent<CheckpointData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkPointData.UpdateCheckpoint(transform.position);
        }
    }
}
