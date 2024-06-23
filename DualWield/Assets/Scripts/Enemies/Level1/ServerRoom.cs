using UnityEngine;
using TMPro;

public class ServerRoom : BaseEnemy
{
    [Header("Setup Connection")]
    [SerializeField] GameObject door;
    [SerializeField] private TextMeshPro[] lineConnections;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damageAmount)
    {
        if (base.health - damageAmount <= 0)
        {
            DisableDoor();
        }
        base.TakeDamage(damageAmount);
    }

    void DisableDoor()
    {
        door.SetActive(false);

        foreach (TextMeshPro connection in lineConnections)
        {
            connection.color = Color.green;
        }
    }
}
