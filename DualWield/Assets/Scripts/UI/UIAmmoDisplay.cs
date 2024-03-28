using System;
using TMPro;
using UnityEngine;

public class UIAmmoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    private float currentAmmoAmount;

    private void Start()
    {
        SetAmmoDisplay(100, true);
    }

    private void Update()
    {
        // When ammo gets low
        ammoDisplay.color = GetAmmoDisplay() <= 20 ? Color.red : Color.white;
    }

    /// <summary>
    /// Sets ammo display in the main game. Saves amount in memory (currentAmmoAmount).
    /// </summary>
    /// <param name="amount">Sets display to amount% (example: 100%)</param>
    /// <param name="isRefill">True, amount refills ammo. False, amount depletes ammo.</param>
    /// <returns name="bool">Returns true if successful, false if unsuccessful</returns>
    public bool SetAmmoDisplay(float amount, bool isRefill)
    {
        if (currentAmmoAmount <= 0 && !isRefill) return false; // if ammo at zero
        
        currentAmmoAmount = amount;
        ammoDisplay.text = amount + "%";
        return true;
    }
    /// <summary>
    /// Gets ammo amount shown on screen (and essentially in UI memory).
    /// </summary>
    /// <returns>Returns current ammo amount known to the UI.</returns>
    public float GetAmmoDisplay()
    {
        return currentAmmoAmount;
    }
}
