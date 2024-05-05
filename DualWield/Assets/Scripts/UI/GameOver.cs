using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] private PlayerCharacter playerCharacter;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
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

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
 