using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateOnClear : MonoBehaviour
{
    [SerializeField] private Transform enemies;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject doorCam;
    [SerializeField] private CameraManager cameraManager;

    // Update is called once per frame
    void Update()
    {
        EnemyCheck();
    }

    void EnemyCheck()
    {
        if (enemies.childCount == 0)
        {
            audioSource.PlayOneShot(audioClip, 2.0f);
            cameraManager.TriggerDoorCam(doorCam);
            door.SetActive(false);
        }
    }
}
