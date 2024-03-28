using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPickupPopup : MonoBehaviour
{
    [SerializeField] private GameObject UIPopup;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // If null, there is no safety net.
    }
    
    void LateUpdate()
    {
        UIPopup.transform.LookAt(mainCamera.transform);
        UIPopup.transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        UIPopup.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        UIPopup.SetActive(false);
    }
}
