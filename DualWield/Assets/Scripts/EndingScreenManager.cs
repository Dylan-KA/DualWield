using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreenManager : MonoBehaviour
{
    public GameObject winningText;
    private float endTime = 6f;
    private float nextScreenTimer = 4f;

    public void Start()
    {
        StartCoroutine(WinTextEnable());
    }

    public IEnumerator WinTextEnable() 
    {
        float endTimer = endTime;
        yield return new WaitForSeconds(endTimer);
        winningText.SetActive(true);
        winningText.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(nextScreenTimer);
        SceneManager.LoadScene(0);
    }
}
