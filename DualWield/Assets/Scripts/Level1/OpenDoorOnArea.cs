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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject doorCam;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private TextMeshPro[] lineConnections;
    private bool isOpen = false;

    private void Start()
    {
        if (!door)
        {
            Debug.LogError("Assign a door to the following area trigger: " + gameObject.name, this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpen)
        {
            if (other.CompareTag("Enemy") && whichEntity == OnArea.Enemy)
            {
                isOpen = true;
                audioSource.PlayOneShot(audioClip, 2.0f);
                door.GetComponent<Animator>().SetBool("isOpen", isOpen);
                cameraManager.TriggerDoorCam(doorCam);
                foreach (TextMeshPro connection in lineConnections)
                {
                    connection.color = Color.green;
                }
            }
            else if (other.CompareTag("Player") && whichEntity == OnArea.Player)
            {
                isOpen = true;
                door.GetComponent<Animator>().SetBool("isOpen", isOpen);
            }
        }
    }
}

enum OnArea
{
    Player,
    Enemy
}
