using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LibraryContacts : MonoBehaviour
{
    // A list of game objects with collider2d components
    [SerializeField] private List<GameObject> gameObjects;

    [SerializeField] private GameObject lastBubble;

    // camera's animator
    [SerializeField] private Animator camera;
    
    // A variable to store the mouse position
    private Vector2 mousePosition;

    // A variable to keep track of how many game objects have been clicked
    private int clickedCount = 0;

    // A variable to keep track of how many game objects have been deactivated
    private int deactivatedCount = 0;

    
    
    
    // A method to update the scene every frame
    private void Update()
    {
        // Get the mouse position in world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position to the scene
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            // Check if the ray hit a collider2d
            if (hit.collider != null)
            {
                // Get the game object that was hit
                GameObject gameObject = hit.collider.gameObject;

                // Check if the game object is in the list of game objects
                if (gameObjects.Contains(gameObject))
                {
                    // Call the OnClick method on the game object
                    OnClick(gameObject);
                }
                // Check if the game object is the first child of a game object in the list
                else if (gameObjects.Contains(gameObject.transform.parent.gameObject))
                {
                    // Call the OnClickChild method on the game object
                    OnClickChild(gameObject);
                }
            }
        }
    }
    

    // A method to handle the click event on a game object
    private async void OnClick(GameObject gameObject)
    {
        // Activate the two children of the game object
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);

        // Deactivate the collider of the game object
        gameObject.GetComponent<Collider2D>().enabled = false;

        // Deactivate the colliders of the other game objects
        foreach (GameObject go in gameObjects)
        {
            if (go != gameObject)
            {
                go.GetComponent<Collider2D>().enabled = false;
            }
        }

        // Increment the clicked count
        clickedCount++;
        
        // Start a coroutine to deactivate the game object after 1 seconds
        StartCoroutine(DeactivateAfterSeconds(gameObject, 1f));
        
        // deactivate and activate the collider of child
        gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = false;
        await Task.Delay(3000);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>().enabled = true;
        
    }

    // A method to handle the click event on the first child of a game object
    private async void OnClickChild(GameObject child)
    {
        // Get the parent game object
        GameObject parent = child.transform.parent.gameObject;

        // Deactivate the two children of the parent game object
        parent.transform.GetChild(0).gameObject.SetActive(false);
        parent.transform.GetChild(1).gameObject.SetActive(false);

        // Deactivate the collider of the parent game object permanently
        parent.GetComponent<Collider2D>().enabled = false;

        // Reactivate the colliders of the other game objects
        foreach (GameObject go in gameObjects)
        {
            if (go != parent)
            {
                go.GetComponent<Collider2D>().enabled = true;
            }
        }

        // Increment the deactivated count
        deactivatedCount++;

        // Check if all game objects have been deactivated
        if (deactivatedCount == gameObjects.Count)
        {
            await Task.Delay(1000);

            StartNewScene();
            
        }
    }

    // A coroutine to deactivate a game object after a given number of seconds
    private IEnumerator DeactivateAfterSeconds(GameObject gameObject, float seconds)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<Collider2D>().enabled = false;
        // Change the color and transparency of the game object to dark and less opaque
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.r *= 0.5f; // Reduce red component by half
        color.g *= 0.5f; // Reduce green component by half
        color.b *= 0.5f; // Reduce blue component by half
        color.a *= 0.8f; // Reduce alpha component by 20%
        gameObject.GetComponent<SpriteRenderer>().color = color;
        
    }

    private async void StartNewScene()
    {
        camera.SetTrigger("Zoom");
        await Task.Delay(3000);
        lastBubble.SetActive(true);
        await Task.Delay(4000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
