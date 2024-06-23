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
    private UIDocument levelSelect;

    private Button playButton;
    //private Button settingsButton;
    private Button creditsButton;
    private Button quitButton;
    private List<Button> allButtons = new List<Button>();

    [SerializeField] GameObject mainMenuCamTransform;
    [SerializeField] GameObject creditsCamTransform;
    float cameraLerp = 0f;
    int lerpDirection = -1;

    void Awake()
    {
        document = GetComponent<UIDocument>();

        playButton = document.rootVisualElement.Q("PlayButton") as Button;
        playButton.RegisterCallback<ClickEvent>(OnPlayClicked);
        //settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        //settingsButton.RegisterCallback<ClickEvent>(OnSettingsClicked);
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

    private void Start()
    {
        levelSelect = FindObjectOfType<LevelSelect>().GetComponent<UIDocument>();
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    private void OnPlayClicked(ClickEvent evt)
    {
        levelSelect.rootVisualElement.visible = true;
        document.rootVisualElement.visible = false;
    }

    /*private void OnSettingsClicked(ClickEvent evt)
    {
        
    }*/

    private void OnCreditsClicked(ClickEvent evt)
    {
        // no more rick roll ;(
        //Application.OpenURL("https://www.youtube.com/watch?v=xvFZjo5PgG0&ab_channel=Duran");

        lerpDirection = 1;
    }

    private void OnQuitClicked(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnAnyButtonClicked(ClickEvent evt)
    {

    }

    private void Update()
    {
        cameraLerp += lerpDirection * Time.deltaTime;
        cameraLerp = Mathf.Clamp(cameraLerp, 0, 1);
        Vector3 newPosition = Vector3.Lerp(mainMenuCamTransform.transform.position, creditsCamTransform.transform.position, cameraLerp);
        Quaternion newQuaternion = Quaternion.Lerp(mainMenuCamTransform.transform.rotation, creditsCamTransform.transform.rotation, cameraLerp);
        Camera.main.transform.position = newPosition;
        Camera.main.transform.rotation = newQuaternion;
    }
}
