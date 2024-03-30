using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private GameManager UIAmmoDisplay;
    private bool success;
    
    // Start is called before the first frame update
    void Start()
    {
        UIAmmoDisplay = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            success = UIAmmoDisplay.DrainAmmo(1);
            if (!success) { Debug.Log("Set display failed - already at zero ammo."); }
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            success = UIAmmoDisplay.DrainAmmo(1);
            if (!success) { Debug.Log("Set display failed - already at zero ammo."); }
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
           UIAmmoDisplay.RefillAmmo();
        }
    }
}
