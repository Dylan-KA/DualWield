using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour
{
    private UIDocument document;
    [SerializeField] private UIDocument previousDocument;

    private DropdownField qualityDropDown;
    private Slider mouseSensitivitySlider;
    private SliderInt fieldOfViewSlider;
    private Button backButton;
    private List<Button> allButtons = new List<Button>();

    void Awake()
    {
        document = GetComponent<UIDocument>();

        qualityDropDown = document.rootVisualElement.Q<DropdownField>("QualityDropDown");
        qualityDropDown.RegisterCallback<ChangeEvent<string>>(OnQualityChanged);
        mouseSensitivitySlider = document.rootVisualElement.Q<Slider>("MouseSensitivitySlider");
        mouseSensitivitySlider.RegisterCallback<ChangeEvent<float>>(OnMouseSensitivityChanged);
        fieldOfViewSlider = document.rootVisualElement.Q<SliderInt>("FieldOfViewSlider");
        fieldOfViewSlider.RegisterCallback<ChangeEvent<int>>(OnFieldOfViewChanged);

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
        document.rootVisualElement.visible = false;

        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
        fieldOfViewSlider.value = PlayerPrefs.GetInt("FieldOfView", 60);
        qualityDropDown.value = PlayerPrefs.GetString("Quality", "High");

        if (qualityDropDown.value == "Very Low")
            QualitySettings.SetQualityLevel(0);
        else if (qualityDropDown.value == "Low")
            QualitySettings.SetQualityLevel(1);
        else if (qualityDropDown.value == "Medium")
            QualitySettings.SetQualityLevel(2);
        else if (qualityDropDown.value == "High")
            QualitySettings.SetQualityLevel(3);
        else if (qualityDropDown.value == "Very High")
            QualitySettings.SetQualityLevel(4);
        else if (qualityDropDown.value == "Ultra")
            QualitySettings.SetQualityLevel(5);
    }

    private void OnBackClicked(ClickEvent evt)
    {
        previousDocument.rootVisualElement.visible = true;
        document.rootVisualElement.visible = false;
    }

    void OnQualityChanged(ChangeEvent<string> evt)
    {
        PlayerPrefs.SetString("Quality", evt.newValue);

        if (evt.newValue == "Very Low")
            QualitySettings.SetQualityLevel(0);
        else if (evt.newValue == "Low")
            QualitySettings.SetQualityLevel(1);
        else if (evt.newValue == "Medium")
            QualitySettings.SetQualityLevel(2);
        else if (evt.newValue == "High")
            QualitySettings.SetQualityLevel(3);
        else if (evt.newValue == "Very High")
            QualitySettings.SetQualityLevel(4);
        else if (evt.newValue == "Ultra")
            QualitySettings.SetQualityLevel(5);
    }

    void OnMouseSensitivityChanged(ChangeEvent<float> evt)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", evt.newValue);
    }

    void OnFieldOfViewChanged(ChangeEvent<int> evt)
    {
        PlayerPrefs.SetInt("FieldOfView", evt.newValue);
    }

    private void OnAnyButtonClicked(ClickEvent evt)
    {

    }
}
