using TMPro;
using UnityEngine;

public class OpenDoorOnPickup : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Light[] lights;
    [SerializeField] private TextMeshPro[] lineConnections;
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            door.SetActive(false);

            foreach (Light var_light in lights)
            {
                var_light.color = Color.red;
                var_light.intensity = 8.0f;
            }

            foreach (TextMeshPro connection in lineConnections)
            {
                connection.color = Color.green;
            }
        }
    }
}
