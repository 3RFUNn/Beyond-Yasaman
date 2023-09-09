using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Season1()
    {
        SceneManager.LoadScene("S1_Scene1");
    }
    public void Season2()
    {
        SceneManager.LoadScene("S2_Scene1");
    }
    public void Season3()
    {
        SceneManager.LoadScene("S3_Scene1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
