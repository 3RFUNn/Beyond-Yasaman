using System.Threading.Tasks;
using UnityEngine;

public class InstagramLogo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer objectColor;
    [SerializeField] private GameObject[] queue;
    [SerializeField] private Animator _animator;
    private static bool open = true;
    // This function is called every frame
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
                if (hit.collider == GetComponent<Collider2D>())
                {
                    // Call your custom function here
                    OpenApp();
                    
                }
            }
        }
    }

    // Your custom function
    private async void OpenApp()
    {
        if (objectColor.color.a == 1)
        {
            if (open)
            {
                _animator.SetTrigger("Open");
                open = false;
                await Task.Delay(1500);
                queue[0].SetActive(false);
                queue[1].SetActive(true);
                await Task.Delay(1000);
            }
        }
    }
}

