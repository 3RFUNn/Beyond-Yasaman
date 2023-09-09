using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandDesk : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Collider2D phone;
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Convert the mouse position to a world point
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform a 2D raycast at the click position
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // Check if the raycast hit something
            if (hit.collider != null)
            {
                // Check if the hit object is the one you want to detect clicks on
                if (hit.collider == phone)
                {
                    phone.enabled = false;
                    _animator.speed = 0;
                    
                    NextScene();

                }
            }
        }

    }
    private async void NextScene()
    {
        await Task.Delay(2000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    
}
