using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BookPurchase : MonoBehaviour
{
    // The array of game objects with animations and colliders
    [SerializeField] private GameObject[] gameObjects;
    
    // the phone gameobject
    [SerializeField] private GameObject Phone;

    // The index of the current game object that is clicked
    private int currentIndex = 0;

    [SerializeField] private AudioSource button;

    // The new game object that is activated after all four game objects are deactivated
    [SerializeField] private GameObject newGameObject;

    // The last game object that is activated after the new game object is deactivated
    [SerializeField] private GameObject lastGameObject;

    // The time to wait before deactivating the current game object
    [SerializeField] private float waitTime = 1.5f;

    // The time to wait before deactivating the new game object
    [SerializeField] private float newWaitTime = 2f;

    // The time to wait before loading the next scene
    [SerializeField] private float lastWaitTime = 3f;

    // The animator component of the current game object
    private Animator animator;

    // The collider component of the current game object
    private Collider2D collider;

    // The animator component of the new game object
    private Animator newAnimator;

    // The collider component of the new game object
    private Collider2D newCollider;
    

    // The text component that shows the counter
    [SerializeField] private Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        // Set the current game object to the first one in the array
        SetCurrentGameObject(0);

        // Deactivate the new game object and the last game object at the start
        newGameObject.SetActive(false);
        lastGameObject.SetActive(false);

        // Set the counter text to show zero at the start
        counterText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse position overlaps with the current collider
            if (collider.OverlapPoint(mousePosition))
            {
                
                // Set the trigger on the animator to play the animation
                animator.SetTrigger("purchase");
                animator.gameObject.GetComponent<Collider2D>().enabled = false;
                button.Play();

                // Disable the colliders of other game objects in the array
                DisableOtherColliders();

                // Start a coroutine to deactivate the current game object after a delay and update the counter text
                StartCoroutine(DeactivateCurrentGameObjectAndUpdateCounter());
                
                // Start a coroutine to activate other colliders after a delay of 0.5 seconds
                StartCoroutine(ActivateOtherColliders());
            }

            // Check if all four game objects are deactivated and the new game object is active
            if (currentIndex == 4 && newGameObject.activeSelf)
            {
                // Check if the mouse position overlaps with the new collider
                if (newCollider.OverlapPoint(mousePosition))
                {
                    button.Play();
                    // Set the trigger on the new animator to play the animation
                    newAnimator.SetTrigger("Nahayi");

                    // Start a coroutine to deactivate the new game object after a delay and update the counter text
                    StartCoroutine(DeactivateNewGameObjectAndUpdateCounter());
                }
            }
        }
    }
    
    
    // A method to disable the colliders of other game objects in the array except for the current one
    void DisableOtherColliders()
    {
        // Loop through all the game objects in the array
        for (int i = 0; i < gameObjects.Length; i++)
        {
            // Check if the index is not equal to the current index
            if (i != currentIndex)
            {
                // Get the collider component of the game object at that index
                Collider2D otherCollider = gameObjects[i].GetComponent<Collider2D>();

                // Disable that collider component
                otherCollider.enabled = false;
            }
        }
    }

    // A coroutine to activate other colliders after a delay of 0.5 seconds 
    IEnumerator ActivateOtherColliders()
    {
        // Wait for 0.5 seconds before activating 
        yield return new WaitForSeconds(0.5f);

        // Loop through all the game objects in array 
        for (int i = 0; i < gameObjects.Length; i++)
        {
            // Check if the index is not equal to the current index 
            if (i != currentIndex)
            {
                // Get the collider component of the game object at that index 
                Collider2D otherCollider = gameObjects[i].GetComponent<Collider2D>();

                // Enable that collider component 
                otherCollider.enabled = true;
            }
        }
    }

    // A method to set the current game object and its components based on an index
    void SetCurrentGameObject(int index)
    {
        // Get the game object from the array using the index
        GameObject gameObject = gameObjects[index];

        // Set the current game object, animator and collider to the ones from the array
        animator = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<Collider2D>();
    }

    

    // A coroutine to deactivate the current game object after a delay, activate the next one or load a new scene, and update the counter text by adding one 
    IEnumerator DeactivateCurrentGameObjectAndUpdateCounter()
    {
        // Wait for a specified amount of time before deactivating 
        yield return new WaitForSeconds(waitTime);

        // Deactivate the current game object 
        animator.gameObject.SetActive(false);

        // Increment the current index by one 
        currentIndex++;

        // Update the counter text by adding one
        counterText.text = (int.Parse(counterText.text) + 1).ToString();

        // Check if there are more game objects in the array 
        if (currentIndex < gameObjects.Length)
        {
            // Set the current game object and its components to be next one in array 
            SetCurrentGameObject(currentIndex);
        }
        else 
        {
            // Activate a new game object and its components 
            ActivateNewGameObject();
        }
    }

    // A method to activate the new game object and its components
    void ActivateNewGameObject()
    {
        // Activate the new game object
        newGameObject.SetActive(true);
        
        // Change the color and transparency of the game object to dark and less opaque
        Color color = Phone.GetComponent<SpriteRenderer>().color;
        color.r *= 0.5f; // Reduce red component by half
        color.g *= 0.5f; // Reduce green component by half
        color.b *= 0.5f; // Reduce blue component by half
        Phone.GetComponent<SpriteRenderer>().color = color;
        
        // Get the animator and collider components of the new game object
        newAnimator = newGameObject.GetComponent<Animator>();
        newCollider = newGameObject.GetComponent<Collider2D>();
    }

    // A coroutine to deactivate the new game object after a delay, activate the last one, and update the counter text by adding one
    IEnumerator DeactivateNewGameObjectAndUpdateCounter()
    {
        // Wait for a specified amount of time before deactivating
        yield return new WaitForSeconds(newWaitTime);

        // Deactivate the new game object
        newAnimator.gameObject.SetActive(false);

        // Update the counter text by adding one
        counterText.text = (int.Parse(counterText.text)).ToString();

        // Activate the last game object and its component
        ActivateLastGameObject();
    }

    // A method to activate the last game object and its component
    void ActivateLastGameObject()
    {
        counterText.enabled = false;
        // Activate the last game object
        lastGameObject.SetActive(true);
        
        // Start a coroutine to load the next scene after a delay
        StartCoroutine(LoadNextScene());
    }

    // A coroutine to load the next scene after a delay
    IEnumerator LoadNextScene()
    {
        // Wait for a specified amount of time before loading
        yield return new WaitForSeconds(lastWaitTime);

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by adding one to the current scene index
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
