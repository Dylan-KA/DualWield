using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpResource : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private AudioClip ammoPickupSFX;
    [SerializeField] float resource;
    // Adjustable parameters for the bounce and rotation
    float bounceAmplitude = 0.25f; // Height of the bounce
    float bounceFrequency = 3.0f;   // Speed of the bounce
    float rotationSpeed = 50.0f;    // Rotation speed in degrees per second
    // Initial position to calculate bounce
    private Vector3 initialPosition;

    private void Start()
    {
        gameManager = GameManager.Instance;
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Bounce and rotate Ammo
        float newY = initialPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            bool success = false; // default
            switch (resourceType)
            {
                case ResourceType.ammo:
                    success = gameManager.RefillAmmoCustom(100f); // used to be RefillAmmoCustom(resource)
                    break;
                case ResourceType.health:
                    other.gameObject.GetComponent<BaseCharacter>().RecoverHP(resource);
                    break;
            }
            
            if (success) {
                gameObject.SetActive(false);
                AudioSource.PlayClipAtPoint(ammoPickupSFX, transform.position);
                Invoke("Respawn", 30f);
            }
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
