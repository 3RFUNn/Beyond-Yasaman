using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OpenBook : MonoBehaviour
{
    [SerializeField] private GameObject[] conversation;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject text;
    // The current event index
    private int eventIndex = 0;
    private static bool action = true;

// The total number of events
    private int eventCount = 12;

// The array of delegates for the events
    private System.Action[] events;

    private void Start()
    {
        // Initialize the events array
        events = new System.Action[eventCount];
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                events[i] = EventOne;
            }else {
                events[i] = EventTwo;
            }
        }

        events[3] = Book1;
        events[4] = Yasaman1;
        events[5] = reset1;
        events[6] = Yasaman2;
        events[7] = reset2;
        events[8] = Book3;
        events[9] = Yasaman3;
        events[10] = reset3;
        events[11] = exit;




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
                if (hit.collider == GetComponent<BoxCollider2D>() && action)
                {
                    events[eventIndex].Invoke();
                    eventIndex++;
                    delay();
                   
                    
                    
                }
            }
        }
    }

    private async void delay()
    {
        action = false;
        await Task.Delay(2000);
        action = true;
    }
    private void EventOne()
    {
        _animator.SetTrigger("Open");
        text.SetActive(false);
        _audioSource.Play();
    }

    private void EventTwo()
    {
        _audioSource.Play();
        _animator.SetTrigger("Page");
        
    }

    private void Book1()
    {
        conversation[0].SetActive(true);
        
    }

    private void Yasaman1()
    {
        conversation[1].SetActive(true);
    }

    private async void reset1()
    {
        conversation[0].SetActive(false);
        conversation[1].SetActive(false);
        await Task.Delay(100);
        conversation[2].SetActive(true);
        
    }
    

    private async void Yasaman2()
    {
        conversation[3].SetActive(true);
        await Task.Delay(1500);
        conversation[4].SetActive(true);
    }

    private async void reset2()
    {
        conversation[2].SetActive(false);
        conversation[3].SetActive(false);
        conversation[4].SetActive(false);
        await Task.Delay(500);
        conversation[5].SetActive(true);
        

    }
    private void Book3()
    {
        conversation[6].SetActive(true);


    }

    private async void Yasaman3()
    {
        conversation[5].SetActive(false);
        conversation[6].SetActive(false);
        conversation[7].SetActive(true);
        await Task.Delay(1500);
        conversation[8].SetActive(true);


    }

    private void reset3()
    {
        conversation[9].SetActive(true);
        
        
    }

    private void exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
