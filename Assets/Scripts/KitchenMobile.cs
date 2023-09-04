using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenMobile : MonoBehaviour
{
    [SerializeField] private GameObject[] _phone;
    [SerializeField] private Collider2D _collider;
    
    private bool onetime = true;

    private void Start()
    {
        StartCoroutine(WakePhone());

    }

    private void Update()
    {
        if (onetime)
        {
            ClickOn();
        }
    }
    
    private void ClickOn()
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
                if (hit.collider.gameObject == gameObject)
                {
                    LoadNext();  
                    onetime = false;
                     
                }
            }
        }
    }


    private IEnumerator WakePhone()
    {
        yield return new WaitForSeconds(4);
        _phone[0].SetActive(false);
        _phone[1].SetActive(true);
        yield return new WaitForSeconds(1);
        _collider.enabled = true;
        StartCoroutine(Phone());

    }

    private IEnumerator Phone()
    {
        yield return new WaitForSeconds(1);
        _phone[0].SetActive(true);
        _phone[1].SetActive(false);
        yield return new WaitForSeconds(1);
        _phone[0].SetActive(false);
        _phone[1].SetActive(true);
        yield return new WaitForSeconds(1);

        StartCoroutine(Phone());

    }
    
    
    private async void LoadNext()
    {
        await Task.Delay(1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
