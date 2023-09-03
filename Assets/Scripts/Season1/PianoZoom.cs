using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PianoZoom : MonoBehaviour
{
    // The name of the animation clip to play
    [SerializeField] private Animator animator;

    // The collider2d component attached to the gameobject
    [SerializeField] private Collider2D collider2d;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        // Check if the user clicks the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse position overlaps with the collider2d
            if (collider2d.OverlapPoint(mousePosition))
            {
                // Play the animation
                animator.SetTrigger("Zoom");

                // Invoke the LoadNextScene method after the animation duration
                Invoke("LoadNextScene",2f);
            }
        }
    }

    // Load the next scene by adding 1 to the current scene index
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
