using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOHover : MonoBehaviour
{
    [SerializeField] float bounceAmplitude = 0.25f; // Height of the bounce
    [SerializeField] float bounceFrequency = 3.0f;   // Speed of the bounce
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
