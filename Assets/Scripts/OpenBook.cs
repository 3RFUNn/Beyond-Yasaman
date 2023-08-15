using UnityEngine;
using UnityEngine.UIElements;

public class OpenBook : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject text;
    // The current event index
    private int eventIndex = 0;

// The total number of events
    private int eventCount = 3;

// The array of delegates for the events
    private System.Action[] events;

    private void Start()
    {
        // Initialize the events array
        events = new System.Action[eventCount];
        events[0] = EventOne;
        events[1] = EventTwo;
        events[2] = EventThree;
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
                if (hit.collider == GetComponent<BoxCollider2D>())
                {
                    events[eventIndex].Invoke();
                    eventIndex++;
                   
                    
                    
                }
            }
        }
    }
    
    private void EventOne()
    {
        _animator.SetTrigger("Open");
        text.SetActive(false);
    }

    private void EventTwo()
    {
        // Do something for the second event
        Debug.Log("Second event triggered");
    }

    private void EventThree()
    {
        // Do something for the third event
        Debug.Log("Third event triggered");
    }
    
}
