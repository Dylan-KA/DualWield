using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionObjective : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionText;
    [SerializeField] private string missionString;
    [SerializeField] private float textDuration;
    private bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        missionText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!played)
        {
            ShowText();
            played = true;
        }
    }

    private void ShowText()
    {
        missionText.text = missionString;
        Invoke("RemoveText", textDuration);
    }

    private void RemoveText()
    {
        missionText.text = "";
    }
}
