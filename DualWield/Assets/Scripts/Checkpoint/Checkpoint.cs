using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointData checkPointData;
    private WeaponHotbar unlockedWeaponsList;
    public GameObject player;

    [SerializeField] private bool isOverride = false;
    [SerializeField] private bool hasWind;
    [SerializeField] private bool hasFlame;
    [SerializeField] private bool hasRocket;
    [SerializeField] private bool hasFreeze;

    // Start is called before the first frame update
    void Start()
    {
        checkPointData = GameObject.FindGameObjectWithTag("Checkpoint Data").GetComponent<CheckpointData>();
        unlockedWeaponsList = FindObjectOfType<WeaponHotbar>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(RepositionPlayer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOverride)
            {
                checkPointData.UpdateCheckpoint(transform.position, unlockedWeaponsList.GunsUnlocked);
            }
            else
            {
                bool[] gunUnlocked = { hasWind, hasFlame, hasRocket, hasFreeze };
                checkPointData.UpdateCheckpoint(transform.position, gunUnlocked);
            }
        }
    }

    private IEnumerator RepositionPlayer()
    {
        float wait = 0.01f;
        yield return new WaitForSeconds(wait);

        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance <= 5f)
        {
            player.GetComponent<Transform>().position = gameObject.transform.position;
            player.GetComponent<Transform>().rotation = gameObject.transform.rotation;
        }
    }
}
