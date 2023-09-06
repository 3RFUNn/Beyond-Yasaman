using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenClock : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _camera;
    [SerializeField] private Collider2D _clock;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Clock());
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
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
                if (hit.collider == _clock)
                {
                    _clock.enabled = false;
                    _camera.SetTrigger("Zoom");
                    _animator.SetTrigger("Off");
                    NextScene();
                }
            }
        }
    }

    private IEnumerator Clock()
    {
        yield return new WaitForSeconds(3);
        _animator.SetTrigger("Clock");
        yield return new WaitForSeconds(1);
        _clock.enabled = true;
    }

    private async void NextScene()
    {
        await Task.Delay(2000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
