using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestRoom : MonoBehaviour
{
   [SerializeField] private WeaponType TestWeapon;
   [SerializeField] private GameObject ParentWeapon;

   //Used for Level 1
   [SerializeField] private GameObject door;
   [SerializeField] private Light[] lights;
   [SerializeField] private TextMeshPro[] lineConnections;

    private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         PlayerCharacter PC = other.gameObject.GetComponent<PlayerCharacter>();
         PC.EquipWeapon(TestWeapon, Hand.Right);
         PC.EquipWeapon(TestWeapon, Hand.Left);
         FindObjectOfType<WeaponHotbar>().UnlockWeapon(TestWeapon);
         FindObjectOfType<GameManager>().RefillAmmo();
         FindObjectOfType<CheckpointData>().UnlockWeapon(TestWeapon);
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
      }
   }
}
