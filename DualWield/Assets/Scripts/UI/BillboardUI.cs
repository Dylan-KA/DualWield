using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}