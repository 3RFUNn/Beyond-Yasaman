using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private string Scene;
    
    public static Music instance;

    void Awake()
    {
        // Check if the instance already exists
        if (instance == null)
        {
            // If not, assign the current object to it
            instance = this;
            // Call DontDestroyOnLoad to make the object persistent
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If it does, destroy the current object to avoid duplicates
            Destroy(gameObject);
        }
    }


    public void PlayMusic()
    {
        if(_audioSource == null) return;
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        if(_audioSource == null) return;
        _audioSource.Pause();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(Scene))
        {
            Destroy(this.gameObject);
        }
    }
}
