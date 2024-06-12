using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Camera WeaponCamera;
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject AmmoBar;
    [SerializeField] private GameObject CrossHair;
    [SerializeField] private float DoorCamSeconds;
    private GameObject CurrentDoorCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerDoorCam(GameObject DoorCamera)
    {
        DoorCamera.SetActive(true);
        CurrentDoorCamera = DoorCamera;
        PlayerCamera.enabled = false;
        WeaponCamera.enabled = false;
        HealthBar.SetActive(false);
        AmmoBar.SetActive(false);
        CrossHair.SetActive(false);
        Invoke("EndDoorCam", DoorCamSeconds);
    }

    private void EndDoorCam()
    {
        PlayerCamera.enabled = true;
        WeaponCamera.enabled = true;
        HealthBar.SetActive(true);
        AmmoBar.SetActive(true);
        CrossHair.SetActive(true);
        CurrentDoorCamera.SetActive(false);
    }
}
