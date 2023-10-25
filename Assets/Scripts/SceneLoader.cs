using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Menu menu;
    
    
    public void NewGame()
    {
        
       
        SceneManager.LoadScene("S1_Scene0");
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void LoadGame()
    {
        menu.LoadSceneName();
    }
}
