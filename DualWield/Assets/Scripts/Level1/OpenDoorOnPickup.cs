using UnityEngine;

public class OpenDoorOnPickup : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private Light[] lights;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            door.SetActive(false);

            foreach (Light var_light in lights)
            {
                var_light.color = Color.red;
                var_light.intensity = 2.5f;
            }
        }
    }
}
