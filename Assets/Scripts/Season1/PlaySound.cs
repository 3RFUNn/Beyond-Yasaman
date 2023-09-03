using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    
    // The array of sounds to play
    [SerializeField] private AudioClip[] sounds;

    // The delay between each sound in seconds
    [SerializeField] private float delay;
    [SerializeField] private float firstdelay;

    // The audio source component
    [SerializeField] private AudioSource audioSource;

    // The index of the current sound
    private int soundIndex;

    // The coroutine that plays the sounds
    private IEnumerator soundCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
        // Initialize the sound index
        soundIndex = 0;

        // Start the sound coroutine
        soundCoroutine = PlaySounds();
        StartCoroutine(soundCoroutine);
    }

    // A coroutine that plays the sounds with a delay
    private IEnumerator PlaySounds()
    {
        // Loop through the sounds array
        while (soundIndex < sounds.Length)
        {
            // Check if it is the first sound or not
            if (soundIndex == 0)
            {
                // Wait for the first delay
                yield return new WaitForSeconds(firstdelay);
                // Play the current sound
                audioSource.clip = sounds[soundIndex];
                audioSource.Play();
                yield return new WaitForSeconds(delay);
            }
            else
            {
                // Play the current sound
                audioSource.clip = sounds[soundIndex];
                audioSource.Play();
                // Wait for the regular delay
                yield return new WaitForSeconds(delay);
            }
            soundIndex++;
        }
    }
}

