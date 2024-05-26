using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform barTransform;
    [SerializeField] private RawImage[] barImages;
    private Color green = new Color(150f / 255f, 255f / 255f, 150f / 255f);
    //private Color yellow = new Color(255f / 255f, 255f / 255f, 50f / 255f);
    private Color red = new Color(255f / 255f, 100f / 255f, 100f / 255f);

    public void UpdateHealth(float newHealth)
    {
        barTransform.sizeDelta = new Vector2((newHealth / 100f) * 250, 25);

        //if (newHealth >= 50)
        //    barImage.color = Color.Lerp(yellow, green, (newHealth - 50) / 50f);
        //else
        //    barImage.color = Color.Lerp(red, yellow, newHealth / 50f);

        Color newColor = Color.Lerp(red, green, newHealth / 100f);
        foreach (RawImage image in barImages)
        {
            image.color = newColor;
        }
    }
}
