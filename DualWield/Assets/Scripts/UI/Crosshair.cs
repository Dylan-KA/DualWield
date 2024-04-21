using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject[] crosshairs;

    // Called when a new weapon is equipped
    public void UpdateCrosshair(WeaponType leftWeapon, WeaponType rightWeapon)
    {
        for (int i = 0; i < crosshairs.Length; i++)
        {
            if ((int)leftWeapon == i || (int)rightWeapon == i)
                crosshairs[i].SetActive(true);
            else
                crosshairs[i].SetActive(false);
        }
    }
}
