using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWindow : MonoBehaviour
{
    // game object of open window
    [SerializeField] private GameObject window;
    // collider2d of closed window
    [SerializeField] private Collider2D open;

    [SerializeField] private AudioSource windowopen;

    [SerializeField] private GameObject arrow;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Window());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            // Convert the mouse position to a world point
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform a 2D raycast at the click position
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);

            // Check if the raycast hit something
            if (hit.collider != null)
            {
                // Check if the hit object is the one you want to detect clicks on
                if (hit.collider == open)
                {
                    open.enabled = false;
                    windowopen.Play();
                    window.SetActive(true);
                    arrow.SetActive(false);
                    NextScene();
                    

                }
            }
        }
    }

    private IEnumerator Window()
    {
        yield return new WaitForSeconds(12);
        arrow.SetActive(true);
        open.enabled = true;

    }

    private async void NextScene()
    {
        await Task.Delay(4000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
