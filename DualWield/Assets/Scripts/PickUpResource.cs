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
            switch (resourceType)
            {
                case ResourceType.ammo:
                    gameManager.RefillAmmoCustom(resource);
                    break;
                case ResourceType.health:
                    other.gameObject.GetComponent<BaseCharacter>().RecoverHP(resource);
                    break;
            }
            
            Destroy(this.gameObject);
        }
    }
}
