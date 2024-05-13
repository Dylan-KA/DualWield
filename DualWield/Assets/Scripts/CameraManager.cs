using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
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
        Invoke("EndDoorCam", DoorCamSeconds);
    }

    private void EndDoorCam()
    {
        PlayerCamera.enabled = true;
        CurrentDoorCamera.SetActive(false);
    }
}
