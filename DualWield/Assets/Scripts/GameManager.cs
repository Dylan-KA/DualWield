using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // its PascalCase on purpose!

    [CanBeNull]
    [SerializeField] private TextMeshProUGUI ammoDisplay;
    private float currentAmmoAmount;
    
    [Header("Settings")] [Tooltip("When ammo goes below this number, text goes red. Default: 20")]
    [SerializeField] [Range(0, 100)] private int LowAmmoMax;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (!ammoDisplay) { Debug.LogError("Error: Assign the ammo display to the GameManager.", ammoDisplay); }
        RefillAmmo(); // Starting with two weapons. Remove if we don't start with two weapons.
    }
    
    private void SetAmmoDisplay(float amount)
    {
        if (!ammoDisplay) { return; }
        int amountInt = (int)amount;
        ammoDisplay.text = amountInt + "%";
        ammoDisplay.color = GetAmmo() <= LowAmmoMax ? Color.red : Color.white; // When ammo gets low
    }
    
    /// <summary>
    /// Drains ammo. Saves amount in memory (currentAmmoAmount).
    /// </summary>
    /// <param name="ammoUsed">Amount of ammo used by a weapon. (example: -5 -> 95%)</param>
    /// <returns name="bool">Returns true if successful, false if ammo already at zero.</returns>
    public bool DrainAmmo(float ammoUsed)
    {
        if (currentAmmoAmount <= 0) return false; // if ammo at zero
        /* 
         If ammo goes under 0, normalizes back to 0.
         Example: If I shot a weapon that uses 95 ammo, and I'm at 5% left
            then I use a different combination that uses 10 ammo, I'll go under 0.
         */
        if ((currentAmmoAmount -= ammoUsed) <= 0) { currentAmmoAmount = 0; }
        SetAmmoDisplay(currentAmmoAmount);
        return true;
    }
    
    /// <summary>
    /// Refills ammo back to 100%. Saves amount in memory (currentAmmoAmount).
    /// </summary>
    public void RefillAmmo()
    {
        currentAmmoAmount = 100;
        SetAmmoDisplay(100);
    }
    
    /// <summary>
    /// Refills ammo +amount%. Use this if you want a custom amount of ammo instead.
    /// Saves amount in memory (currentAmmoAmount).
    /// </summary>
    /// <param name="amount">Will add amount to the currentAmmoAmount.</param>
    public bool RefillAmmoCustom(float amount)
    {
        if (currentAmmoAmount >= 100) // If we're already at max
        {
            return false;
        }
        if ((currentAmmoAmount += amount) >= 100) // If adding goes over max
        {
            currentAmmoAmount = 100;
        }
        SetAmmoDisplay(currentAmmoAmount);
        return true;
    }
    
    /// <summary>
    /// Gets ammo amount shown on screen (and essentially in GameManager & UI memory).
    /// </summary>
    /// <returns>Returns current ammo amount known to the game & UI.</returns>
    public float GetAmmo()
    {
        return currentAmmoAmount;
    }
}
