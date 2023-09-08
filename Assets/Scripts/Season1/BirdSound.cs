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
            if (currentIndex == 0)
            {
                yield return new WaitForSeconds(0.3f);
                audioSource.clip = audioClips[currentIndex];
                audioSource.Play();
            
            }
            else
            {
                audioSource.clip = audioClips[currentIndex];
                audioSource.Play();
            }
            
            currentIndex++;

            yield return new WaitForSeconds(3.0f); 
        }
    }
}


