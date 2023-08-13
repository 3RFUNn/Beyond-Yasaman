using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    private int currentIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioWithDelay());
    }
    private IEnumerator PlayAudioWithDelay()
    {
        while (currentIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();
            currentIndex++;

            yield return new WaitForSeconds(3.0f); // 2 second delay
        }
    }
}


