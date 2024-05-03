using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpResource : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] private ResourceType resourceType;

    [SerializeField] float resource;

    private void Start()
    {
        gameManager = GameManager.Instance;
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
                Invoke("Respawn", 30f);
            }
        }
    }

    private void Respawn()
    {
        gameObject.SetActive(true);
    }
}
