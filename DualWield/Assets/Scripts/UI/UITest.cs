using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private UIAmmoDisplay UIAmmoDisplay;
    private bool success;
    
    // Start is called before the first frame update
    void Start()
    {
        UIAmmoDisplay = gameObject.GetComponent<UIAmmoDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            float currentAmmo = UIAmmoDisplay.GetAmmoDisplay();
            success = UIAmmoDisplay.SetAmmoDisplay(currentAmmo - 5, false);
            if (!success) { Debug.Log("Set display failed - already at zero ammo."); }
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
           success = UIAmmoDisplay.SetAmmoDisplay(100, true);
        }
    }
}
