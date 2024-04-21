using UnityEngine;
using TMPro;

public class ServerRoom : BaseEnemy
{
    private float currentHealth;
    [Header("Setup Connection")]
    [SerializeField] GameObject door;
    [SerializeField] private TextMeshPro[] lineConnections;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        currentHealth = base.health;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckHealth();
    }

    void CheckHealth()
    {
        if (base.health <= currentHealth)
        {
            currentHealth = base.health; 
        }
        if (currentHealth <= 125)
        {
            door.SetActive(false);
            
            foreach (TextMeshPro connection in lineConnections)
            {
                connection.color = Color.green;
            }
        }
    }
}
