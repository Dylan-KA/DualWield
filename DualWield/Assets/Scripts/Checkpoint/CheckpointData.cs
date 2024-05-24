using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointData : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private int scene;
    private bool[] unlockedWeapons = { false, true, false, false }; // flame, wind, freeze, rocket
    
    private void OnEnable()
    {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

    private void OnDisable()
    {
		SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex == 0)
        {
            ResetCheckpoint();
        }
	}

    public bool IsPlayerRespawnAtCheckpoint()
    {
        if (scene == SceneManager.GetActiveScene().buildIndex && scene != 0)
        {
            return true;
        }
        ResetCheckpoint();
        return false;
    }

    private void ResetCheckpoint()
    {
        scene = 0;
        lastCheckpoint = Vector3.zero;

        unlockedWeapons[0] = false; // no flame
        unlockedWeapons[1] = true; // wind!
        unlockedWeapons[2] = false; // no rocket
        unlockedWeapons[3] = false; // no freeze
    }

    public Vector3 GetPosition()
    {
        return lastCheckpoint; 
    }

    public bool[] GetUnlockedWeapons()
    {
        return unlockedWeapons;
    }

    public void UpdateCheckpoint(Vector3 position)
    {
        lastCheckpoint = position;
        scene = SceneManager.GetActiveScene().buildIndex;
    }

    public void UnlockWeapon(WeaponType weaponType)
    {
        unlockedWeapons[(int)weaponType] = true;
    }
}
