using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragAndDropPuzzle : MonoBehaviour
{
    // The square's collider
    Collider2D collider;

    // The target square where the game object should be placed
    [SerializeField] private Transform targetSquare;

    // The distance between the game object and the target square
    float distance;

    // The sound to play when the game object snaps into place
    //[SerializeField] private AudioClip snapSound;

    // The audio source component
    [SerializeField] private AudioSource _audioSource;

    // A flag to indicate if the game object can be moved
    bool canMove;

    // A flag to indicate if the game object is being dragged
    bool dragging;

    // A flag to indicate if the game object is placed correctly
    bool placed;

    // The other two puzzle pieces
    [SerializeField] private DragAndDropPuzzle piece1;
    [SerializeField] private DragAndDropPuzzle piece2;
    

    void Start()
    {
        // Get the collider component
        collider = GetComponent<Collider2D>();
        
        // Initialize the flags
        canMove = false;
        dragging = false;
        placed = false;
    }

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Check if the mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the game object's collider
            if (collider.OverlapPoint(mousePos))
            {
                // Allow the game object to move
                canMove = true;
            }
        }

        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            // Stop moving the game object
            canMove = false;
            dragging = false;
        }

        // Check if the game object can be moved
        if (canMove)
        {
            // Move the game object to the mouse position
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            // Set the dragging flag to true
            dragging = true;
        }

        // Check if the game object is being dragged
        if (dragging)
        {
            // Calculate the distance between the game object and the target square
            distance = Vector3.Distance(transform.position, targetSquare.position);

            // Check if the distance is less than a threshold value (you can adjust this as you need)
            if (distance < 0.5f)
            {
                // Snap the game object to the center of the target square
                transform.position = targetSquare.position;

                collider.enabled = false;

                

                // Set the placed flag to true
                placed = true;

                // Stop moving and dragging the game object
                canMove = false;
                dragging = false;

                // Check if all three puzzle pieces are placed correctly
                CheckPuzzle();

                // Show a message and play a sound when the puzzle is completed
                PuzzleCompleted();
            }
        }
    }

    private bool CheckPuzzle()
    {
        // If all three puzzle pieces are placed correctly, return true; otherwise, return false.
        if (placed && piece1.placed && piece2.placed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private async void PuzzleCompleted()
    {
        // If all three puzzle pieces are placed correctly, show a message and play a sound.
        if (CheckPuzzle())
        {
            
            _audioSource.Play();
            await Task.Delay(3000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // You can also add other actions here, such as loading another scene.

        }
    }
}