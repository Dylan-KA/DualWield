using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPickupPopup : MonoBehaviour
{
    [SerializeField] private GameObject UIPopup;
    [SerializeField] private GameObject pickup;
    private Camera mainCamera;
    private float timer;

    [Header("Settings")] [Tooltip("Amount of time needed to pick up a weapon.")]
    [SerializeField] private float holdDuration;
    [Tooltip("The weapon of this pickup.")]
    [SerializeField] private WeaponType weaponType;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // If null, there is no safety net.
        timer = 0f;
    }
    
    void LateUpdate()
    {
        UIPopup.transform.LookAt(mainCamera.transform);
        UIPopup.transform.Rotate(0, 180, 0);
    }
    
    private void PickupWeapon(GameObject player, Hand hand)
    {
        PlayerCharacter PlayerCharacter = player.GetComponent<PlayerCharacter>();
        PlayerCharacter.EquipWeapon(weaponType, hand);
        pickup.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        UIPopup.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        // Hold Q for Left Hand
        if (Input.GetKeyDown(KeyCode.Q))
        {
            timer = Time.time;
            Debug.Log("Timer started");
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q held");
            Debug.Log(timer);
            if (Time.time - timer > holdDuration)
            {
                Debug.Log("Q activated");
                timer = float.PositiveInfinity; // Prevents subsequent runs
                
                PickupWeapon(other.gameObject, Hand.Left);
            }
        }
        else
        {
            timer = float.PositiveInfinity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIPopup.SetActive(false);
    }
}
