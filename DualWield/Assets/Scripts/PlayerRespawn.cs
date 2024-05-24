using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private CheckpointData checkpointData;
    private WeaponHotbar weaponHotbar;

    // Start is called before the first frame update
    void Start()
    {
        checkpointData = GameObject.FindGameObjectWithTag("Checkpoint Data").GetComponent<CheckpointData>();
        
        if (checkpointData.IsPlayerRespawnAtCheckpoint())
        {
            transform.position = checkpointData.GetPosition();
        }

        weaponHotbar = FindObjectOfType<WeaponHotbar>();
        bool[] unlockedWeapons = checkpointData.GetUnlockedWeapons();
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            unlockedWeapons[0] = true;
            unlockedWeapons[3] = true;
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            unlockedWeapons[2] = true;
        }
        weaponHotbar.SetUnlockedWeapons(unlockedWeapons);
    }
}
