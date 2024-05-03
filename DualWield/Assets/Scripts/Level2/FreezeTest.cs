using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTest : MonoBehaviour
{
    private bool Test = false;
    [SerializeField] private GameObject TestDoor;

    [SerializeField] private Material CompleteTest;

    private void OnCollisionEnter(Collision other)
    {
        if (Test == false)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (TestDoor)
                {
                    TestDoor.SetActive(false);
                    this.gameObject.GetComponent<MeshRenderer>().material = CompleteTest;

                }
                Test = true;
            }
        }
        
    }
}
