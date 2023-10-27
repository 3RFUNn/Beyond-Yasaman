using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // The key to store and retrieve the scene name
    public const string SceneKey = "SceneName";
    
    private void OnApplicationQuit()
    {
        string sceneName = PlayerPrefs.GetString(SceneKey);
        PlayerPrefs.SetString(SceneKey, sceneName);
        PlayerPrefs.Save();
    }

    // Save the current scene name using PlayerPrefs
    public void SaveSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(SceneKey, sceneName);
        PlayerPrefs.Save();
    }

    
    public static Menu instance;

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

    // Load the last scene name using PlayerPrefs
    public void LoadSceneName()
    {
        
        // Check if the key exists
        if (PlayerPrefs.HasKey(SceneKey))
        {
            // Get the scene name from PlayerPrefs
            string sceneName = PlayerPrefs.GetString(SceneKey);

            // Load the scene if it is not the current one
            if (sceneName != SceneManager.GetActiveScene().name)
            {
                
                SceneManager.LoadScene(sceneName);
                
            }
        }
    }
    
}
