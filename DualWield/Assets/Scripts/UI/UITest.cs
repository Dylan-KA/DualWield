using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    private GameManager gameManager;
    private bool success;
    
    // WeaponDrainCooldown
    private float time;
    private float cooldown = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            success = gameManager.DrainAmmo(1);
            if (!success) { Debug.Log("Set display failed - already at zero ammo."); }
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
           gameManager.RefillAmmo();
        }
    }

    private void Fire()
    {
        // if (Time.time - time < cooldown) { return; }
            
        success = gameManager.DrainAmmo(1);
        if (!success) { Debug.Log("Set display failed - already at zero ammo."); }

        // time = Time.time;
    }
}
