using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class OpenDoorOnArea : MonoBehaviour
{
    [Description("Option: Which entity will be detected by this area trigger.")]
    [SerializeField] private OnArea whichEntity;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject door;
    [SerializeField] private TextMeshPro[] lineConnections;

    private void Start()
    {
        if (!door)
        {
            Debug.LogError("Assign a door to the following area trigger: " + gameObject.name, this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && whichEntity == OnArea.Enemy)
        {
            AudioSource.PlayClipAtPoint(clip, door.transform.position);
            door.SetActive(false);
            foreach (TextMeshPro connection in lineConnections)
            {
                connection.color = Color.green;
            }
        }
        else if (other.CompareTag("Player") && whichEntity == OnArea.Player)
        {
            door.SetActive(false);
        }
    }
}

enum OnArea
{
    Player,
    Enemy
}
