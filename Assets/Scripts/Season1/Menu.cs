using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // The key to store and retrieve the scene name
    private const string SceneKey = "SceneName";

    // The static field that holds the reference to the singleton instance
    private static Menu instance;

    // The property that returns the singleton instance
    public static Menu Instance
    {
        get
        {
            // If the instance is null, try to find an existing SceneSaver object in the scene
            if (instance == null)
            {
                instance = FindObjectOfType<Menu>();
            }

            // If the instance is still null, create a new SceneSaver object and assign it to the instance
            if (instance == null)
            {
                GameObject obj = new GameObject("SceneSaver");
                instance = obj.AddComponent<Menu>();
            }

            // Return the instance
            return instance;
        }
    }

    // The private constructor that prevents other classes from creating new instances
    private Menu()
    {
        // Do nothing
    }

    // Save the current scene name when the game is paused or quit
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveSceneName();
        }
    }

    private void OnApplicationQuit()
    {
        SaveSceneName();
    }

    // Save the current scene name using PlayerPrefs
    private void SaveSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(SceneKey, sceneName);
        PlayerPrefs.Save();
    }

    // Load the last scene name when the game is started
    private void Awake()
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            SaveSceneName();
        }
        // Make sure this object is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
        
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

    public void NewGame()
    {
        SceneManager.LoadScene("S1_Scene1");
    }

    public void Quit()
    {
        Application.Quit();
    }

    
}
