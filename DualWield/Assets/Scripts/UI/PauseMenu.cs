using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private UIDocument document;

    private PlayerCharacter player;
    private Button settingsButton;
    private Button quitButton;
    private List<Button> allButtons = new List<Button>();

    void Start()
    {
        document = GetComponent<UIDocument>();
        document.rootVisualElement.visible = false;

        settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);
        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(OnQuitClicked);

        /*allButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].RegisterCallback<ClickEvent>(OnAnyButtonClicked);
        }*/

        player = FindObjectOfType<PlayerCharacter>();
    }

    private void OnSettingsClicked(ClickEvent evt)
    {
        
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void OnAnyButtonClicked(ClickEvent evt)
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (document.rootVisualElement.visible)
            {
                player.SetMouseLocked(true);
                document.rootVisualElement.visible = false;
                Time.timeScale = 1f;
            }
            else
            {
                player.SetMouseLocked(false);
                document.rootVisualElement.visible = true;
                Time.timeScale = 0f;
            }
        }
    }
}
