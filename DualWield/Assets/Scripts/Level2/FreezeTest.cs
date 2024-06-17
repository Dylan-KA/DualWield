using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTest : MonoBehaviour
{
    private bool Test = false;
    [SerializeField] private GameObject TestDoor;
    [SerializeField] private Material CompleteTest;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject exitCam;

    private void OnCollisionEnter(Collision other)
    {
        if (Test == false)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (TestDoor)
                {
                    Invoke("OpenDoor", 1.5f);
                    cameraManager.TriggerDoorCam(exitCam);
                }
                Test = true;
            }
        }
        
    }

    private void OpenDoor()
    {
        TestDoor.GetComponent<Animator>().SetBool("isOpen", true);
        this.gameObject.GetComponent<MeshRenderer>().material = CompleteTest;
    }
}
