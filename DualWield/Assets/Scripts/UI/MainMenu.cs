using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class MainMenu : MonoBehaviour
{
    private UIDocument document;

    private Button playButton;
    private Button settingsButton;
    private Button creditsButton;
    private Button quitButton;
    private List<Button> allButtons = new List<Button>();

    void Awake()
    {
        document = GetComponent<UIDocument>();

        playButton = document.rootVisualElement.Q("PlayButton") as Button;
        playButton.RegisterCallback<ClickEvent>(OnPlayClicked);
        settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);
        creditsButton = document.rootVisualElement.Q("CreditsButton") as Button;
        creditsButton.RegisterCallback<ClickEvent>(OnCreditsClicked);
        quitButton = document.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(OnQuitClicked);

        allButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].RegisterCallback<ClickEvent>(OnAnyButtonClicked);
        }
    }



    private void OnPlayClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    private void OnSettingsClicked(ClickEvent evt)
    {
        
    }

    private void OnCreditsClicked(ClickEvent evt)
    {
        
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnAnyButtonClicked(ClickEvent evt)
    {

    }
}