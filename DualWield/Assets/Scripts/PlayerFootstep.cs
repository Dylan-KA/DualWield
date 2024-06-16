using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootstep : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array to hold footstep sound clips
    public float minTimeBetweenFootsteps = 0.3f; // Minimum time between footstep sounds
    public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds
    
    private AudioSource audioSource; // Reference to the Audio Source component
    private bool isWalking = false; // Flag to track if the player is walking
    private float timeSinceLastFootstep; // Time since the last footstep sound
    private CharacterController characterController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Get the Audio Source component
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckWalk();
        // Check if the player is walking
        if (isWalking)
        {
            PlayFootstep();
        }
    }

    void CheckWalk()
    {
        if (characterController.velocity.magnitude > 0)
        {
            StartWalking();
        }
        else
        {
            StopWalking();
        }
    }

    void PlayFootstep()
    {
        // Check if enough time has passed to play the next footstep sound
        if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
        {
            // Play a random footstep sound from the array
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(footstepSound);

            timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
        }
    }

    // Call this method when the player starts walking
    void StartWalking()
    {
        isWalking = true;
    }

    // Call this method when the player stops walking
    void StopWalking()
    {
        isWalking = false;
    }
}
