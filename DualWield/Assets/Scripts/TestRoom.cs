using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoom : MonoBehaviour
{
   [SerializeField] private WeaponType TestWeapon;
    [SerializeField] private GameObject ParentWeapon;

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
      }
   }
}
