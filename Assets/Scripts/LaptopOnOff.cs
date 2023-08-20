using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaptopOnOff : MonoBehaviour
{
    [SerializeField] private GameObject off;
    [SerializeField] private GameObject on;
    [SerializeField] private int delay;


    private void Start()
    {
        TurnOn();
    }

    private async void TurnOn()
    {
        await Task.Delay(delay);
        off.SetActive(false);
        @on.SetActive(true);
        
        
    }
    
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hit a collider
            if (hit.collider != null)
            {
                // Check if the hit collider is this one
                if (hit.collider == @on.GetComponent<Collider2D>())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
