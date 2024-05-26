using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private RectTransform barLeft;
    [SerializeField] private RectTransform barRight;


    public void SetBarProgress(float progress)
    {
        barLeft.sizeDelta = new Vector2(progress, 8);
        barRight.sizeDelta = new Vector2(progress, 8);
    }
}
