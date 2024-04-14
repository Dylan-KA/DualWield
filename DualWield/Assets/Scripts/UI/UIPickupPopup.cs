using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPickupPopup : MonoBehaviour
{
    [SerializeField] private GameObject UIPopup;
    [SerializeField] private GameObject pickup;
    [SerializeField] GameManager GameManager;
    private Camera mainCamera;

    [Header("Settings")]
    [Tooltip("The weapon of this pickup.")]
    [SerializeField] private WeaponType weaponType;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameManager.Instance;
        if (!Camera.main) { Debug.LogWarning("Main Camera is missing."); }
        if (!GameManager) { Debug.LogError("GameManager reference is missing from UIManager."); }
        mainCamera = Camera.main; // If null, there is no safety net.
    }
    
    void LateUpdate()
    {
        UIPopup.transform.LookAt(mainCamera.transform);
        UIPopup.transform.Rotate(0, 180, 0);
    }
    
    private void PickupWeapon(GameObject player, Hand hand)
    {
        PlayerCharacter PlayerCharacter = player.GetComponent<PlayerCharacter>();
        if (!PlayerCharacter) return; // fixes null reference error
        PlayerCharacter.EquipWeapon(weaponType, hand);
        GameManager.RefillAmmo();
        pickup.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        UIPopup.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Q))
        {
            PickupWeapon(other.gameObject, Hand.Left);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            PickupWeapon(other.gameObject, Hand.Right);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UIPopup.SetActive(false);
    }
}
