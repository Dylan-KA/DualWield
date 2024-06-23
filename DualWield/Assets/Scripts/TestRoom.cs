using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestRoom : MonoBehaviour
{
   [SerializeField] private WeaponType TestWeapon;
   [SerializeField] private GameObject ParentWeapon;

   //Used for Level 1
   [SerializeField] private GameObject door;
   [SerializeField] private Light[] lights;
   [SerializeField] private TextMeshPro[] lineConnections;

    float bounceAmplitude = 0.18f; // Height of the bounce
    float bounceFrequency = 3.0f;   // Speed of the bounce
    private Vector3 initialPosition;

    // Audio Management //
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] pickup;

    private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         PlayerCharacter PC = other.gameObject.GetComponent<PlayerCharacter>();
         PC.EquipWeapon(TestWeapon, Hand.Right);
         PC.EquipWeapon(TestWeapon, Hand.Left);
         FindObjectOfType<WeaponHotbar>().UnlockWeapon(TestWeapon);
         FindObjectOfType<GameManager>().RefillAmmo();
         Destroy(ParentWeapon);
         if (door != null) { door.SetActive(false); }
         if (lights != null)
         {
            foreach (Light var_light in lights)
            {
                var_light.color = Color.red;
                var_light.intensity = 8.0f;
            }
         }
         if (lineConnections != null)
         {
            foreach (TextMeshPro connection in lineConnections)
            {
                connection.color = Color.green;
            }
         }
         PlayAudio("pickup");

      }
   }

    void PlayAudio(String audioType)
    {
        int i;
        switch (audioType)
        {
            case "pickup":
                i = Random.Range(0, pickup.Length);
                audioSource.PlayOneShot(pickup[i]); return;
            default:
                return;
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        ParentWeapon.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

}
