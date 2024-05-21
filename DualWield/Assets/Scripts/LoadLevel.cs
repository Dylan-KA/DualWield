using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int nextScene;
    //[SerializeField] private bool RequiresKilling; //If the player must kill all enemies before progressing level.
    //[SerializeField] private GameObject ParentOfEnemies; //Parent of a enemies, if all dead then can progress to next level.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(nextScene);
        }
    }
}
