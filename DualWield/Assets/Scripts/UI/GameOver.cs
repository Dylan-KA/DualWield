using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOver : MonoBehaviour
{
    private UIDocument document;

    private Button respawnButton;
    private Button quitButton;
    [SerializeField] private PlayerCharacter playerCharacter;

    [SerializeField] GameObject eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        document = GetComponent<UIDocument>();
        document.rootVisualElement.visible = false;

        respawnButton = document.rootVisualElement.Q("RespawnButton") as Button;
        respawnButton.RegisterCallback<ClickEvent>(RespawnPlayer);
        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(ReturnToMainMenu);
        Time.timeScale = 1f;

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
        eventSystem.SetActive(false);
        document.rootVisualElement.visible = true;
        playerCharacter.SetMouseLocked(false);
        Time.timeScale = 0f;
    }
    public void RespawnPlayer(ClickEvent evt)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu(ClickEvent evt)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
 