using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmSwipe : MonoBehaviour
{
    // The minimum distance to swipe the object
    public float swipeThreshold = 0.5f;
    [SerializeField] private GameObject _gameObject;
    private bool once = true;
    private bool snooze = true;

    // The initial position of the object
    private Vector3 startPosition;

    // The initial position of the mouse
    private Vector3 mouseStartPosition;

    // A flag to indicate if the object is being dragged
    private bool isDragging;

    void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0) && snooze)
        {
            // Store the initial position of the mouse
            mouseStartPosition = Input.mousePosition;

            // Check if the mouse is over the object
            if (IsMouseOverObject())
            {
                // Set the flag to true
                isDragging = true;
            }
        }

        // Check if the left mouse button is released
        if (Input.GetMouseButtonUp(0) && snooze)
        {
            // Set the flag to false
            isDragging = false;

            // Reset the position of the object
            transform.position = startPosition;
        }

        // Check if the object is being dragged
        if (isDragging && snooze)
        {
            // Get the current position of the mouse
            Vector3 mousePosition = Input.mousePosition;

            // Calculate the horizontal distance moved by the mouse
            float deltaX = mousePosition.x - mouseStartPosition.x;

            // Convert it to world units
            deltaX /= Screen.width;

            // Clamp it to the swipe threshold
            deltaX = Mathf.Clamp(deltaX, -swipeThreshold, swipeThreshold);

            // Move the object horizontally by that amount
            transform.position = startPosition + new Vector3(deltaX, 0, 0);

            // Check if the object has reached the end of the swipe
            if (Mathf.Abs(deltaX) == swipeThreshold)
            {
                if (once)
                {
                    
                    _gameObject.GetComponent<Animator>().enabled = false;
                    Loadnextscene();
                    snooze = false;
                    once = false;
                    
                }
            }
        }
    }

    private async void Loadnextscene()
    {
        await Task.Delay(2000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // A helper method to check if the mouse is over the object
    private bool IsMouseOverObject()
    {
        // Get the mouse position in world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the collider of the object
        Collider2D collider = GetComponent<Collider2D>();

        // Check if the collider contains the mouse position
        return collider.OverlapPoint(mouseWorldPosition);
        
    }
}
