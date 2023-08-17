using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompassBlinker : MonoBehaviour
{
    // Reference to the animator component
    private Animator animator;
    [SerializeField] private Animator _animator;

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

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2);
        _animator.SetTrigger("ZoomIn");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has the trigger tag
        if (other.CompareTag(triggerTag))
        {
            // Set the trigger parameter to true
            animator.SetTrigger(triggerName);
            StartCoroutine(ChangeScene());

        }
    }

    // //OnTriggerExit2D is called when the Collider2D other exits the trigger (2D physics only)
    // void OnTriggerExit2D(Collider2D other)
    // {
    //     // Check if the other collider has the trigger tag
    //     if (other.CompareTag(triggerTag))
    //     {
    //         // Reset the trigger parameter to false
    //         animator.SetTrigger(triggerName2);
    //         
    //         
    //     }
    // }
}
