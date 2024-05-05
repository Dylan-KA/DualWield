using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class LevelSelect : MonoBehaviour
{
    private UIDocument document;
    private UIDocument mainMenu;

    private Button level1Button;
    private Button level2Button;
    private Button level3Button;
    private Button backButton;
    private List<Button> allButtons = new List<Button>();

    void Awake()
    {
        document = GetComponent<UIDocument>();

        level1Button = document.rootVisualElement.Q("Level1Button") as Button;
        level1Button.RegisterCallback<ClickEvent>(OnLevel1Clicked);
        level2Button = document.rootVisualElement.Q("Level2Button") as Button;
        level2Button.RegisterCallback<ClickEvent>(OnLevel2Clicked);
        level3Button = document.rootVisualElement.Q("Level3Button") as Button;
        level3Button.RegisterCallback<ClickEvent>(OnLevel3Clicked);
        backButton = document.rootVisualElement.Q("BackButton") as Button;
        backButton.RegisterCallback<ClickEvent>(OnBackClicked);

        allButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].RegisterCallback<ClickEvent>(OnAnyButtonClicked);
        }
    }

    private void Start()
    {
        mainMenu = FindObjectOfType<MainMenu>().GetComponent<UIDocument>();
        document.rootVisualElement.visible = false;
    }

    private void OnLevel1Clicked(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    private void OnLevel2Clicked(ClickEvent evt)
    {
        SceneManager.LoadScene(2);
    }

    private void OnLevel3Clicked(ClickEvent evt)
    {
        SceneManager.LoadScene(3);
    }

    private void OnBackClicked(ClickEvent evt)
    {
        mainMenu.rootVisualElement.visible = true;
        document.rootVisualElement.visible = false;
    }

    private void OnAnyButtonClicked(ClickEvent evt)
    {

    }
}
