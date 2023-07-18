using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassBlinker : MonoBehaviour
{
    // Reference to the animator component
    private Animator animator;

    // Name of the trigger parameter
    private string triggerName = "Green";
    private string triggerName2 = "Light";
    // Tag of the collider that triggers the animation
    private string triggerTag = "Compass";

    // Start is called before the first frame update
    void Start()
    {
        // Get the animator component
        animator = GetComponent<Animator>();
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has the trigger tag
        if (other.CompareTag(triggerTag))
        {
            // Set the trigger parameter to true
            animator.SetTrigger(triggerName);
            Debug.Log("hello");
        }
    }

    // OnTriggerExit2D is called when the Collider2D other exits the trigger (2D physics only)
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the other collider has the trigger tag
        if (other.CompareTag(triggerTag))
        {
            // Reset the trigger parameter to false
            animator.SetTrigger(triggerName2);
            
            Debug.Log("goodbye");
        }
    }
}
