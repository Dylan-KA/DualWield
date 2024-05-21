using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointData : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private int scene;
    
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
    }

    public Vector3 GetPosition()
    {
        return lastCheckpoint; 
    }

    public void UpdateCheckpoint(Vector3 position)
    {
        lastCheckpoint = position;
        scene = SceneManager.GetActiveScene().buildIndex;

    }
}
