
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AlarmSwipe : MonoBehaviour
{
    // The minimum distance to swipe the object
    public float swipeThreshold = 0.5f;

    // The event to invoke when the object is swiped
    public UnityEvent onSwipe;

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
        if (Input.GetMouseButtonDown(0))
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
        if (Input.GetMouseButtonUp(0))
        {
            // Set the flag to false
            isDragging = false;

            // Reset the position of the object
            transform.position = startPosition;
        }

        // Check if the object is being dragged
        if (isDragging)
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
                // Invoke the event
                
                onSwipe.Invoke();
            }
        }
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
