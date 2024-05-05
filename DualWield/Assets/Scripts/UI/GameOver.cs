using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    private PlayerCharacter playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        if (playerCharacter.IsPlayerDead())
        {
            SetGameOver();
        }
    }

    private void SetGameOver()
    {
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void RespawnPlayer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
 