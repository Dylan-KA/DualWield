using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> doorList = new();
    [SerializeField] private List<GameObject> waveList = new();
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject arenaExitCam;
    private bool isArenaStarted = false;
    private int currentWaveCount = 0;
    private int maxWave = 0;

    public int CurrentWaveCount { get => currentWaveCount+1; }
    public int MaxWave { get => maxWave; }

    public void Start()
    {
        SetUpWaveManager();
    }

    private void SetUpWaveManager()
    {
        currentWaveCount = 0;
        maxWave = waveList.Count;
        DisableAllWaves();
    }

    private void Update()
    {
        if (isArenaStarted == true)
        {
            if (IsWaveFinished())
            {
                NextWave();
            }
        }
    }

    public void StartWave()
    {
        isArenaStarted = true;
        EnableCurrentWave();
    }

    private void NextWave()
    {
        currentWaveCount++;
        if (IsArenaFinished())
        {
            EndArena();
        }
        else
        {
            EnableCurrentWave();
        }
    }

    private void EndArena()
    {
        cameraManager.TriggerDoorCam(arenaExitCam);
        ToggleDoor(true);
        isArenaStarted = false;
        DisableAllWaves();
    }

    private void EnableCurrentWave()
    {
        waveList[currentWaveCount].SetActive(true);
    }

    private void DisableAllWaves()
    {
        foreach(GameObject wave in waveList)
        {
            wave.SetActive(false);
        }
    }
    
    private void ToggleDoor(bool isOpen)
    {
        Debug.Log("Toggling Door");
        if (doorList.Count == 0) Debug.Log($"Door Missing in Arena Script: {gameObject.name}");

        foreach(GameObject door in doorList)
        {
            try
            {
                // Animator string name is not final nor if it is a trigger/boolean
                door.GetComponent<Animator>().SetBool("isOpen", isOpen);
            }
            catch
            {
                Debug.LogError($"{door.name} animator function cannot be found || has an error with trigger name");
            }
        }
    }

    private bool IsWaveFinished()
    {
        return waveList[currentWaveCount].GetComponent<Transform>().childCount == 0;
    }

    private bool IsArenaFinished()
    {
        return currentWaveCount == maxWave;
    }
}
