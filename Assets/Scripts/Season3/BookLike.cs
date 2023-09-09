using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookLike : MonoBehaviour
{
    [SerializeField] private Collider2D like;

    [SerializeField] private GameObject phone;

    [SerializeField] private GameObject purchase;

    [SerializeField] private Animator purchaseAnimator;

    [SerializeField] private Animator likeAnimator;

    [SerializeField] private GameObject Instagram;

    [SerializeField] private AudioSource notification;

    [SerializeField] private GameObject message;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
                if (hit.collider == like)
                {
                    like.enabled = false;
                    likeAnimator.SetTrigger("Like");
                    PurchaseNotification();

                }

                if (hit.collider == purchase.GetComponent<Collider2D>())
                {
                    purchase.GetComponent<Collider2D>().enabled = false;
                    OpenNotification();
                    purchaseAnimator.SetTrigger("Notif");
                }
            }
        }
    }

    private async void OpenNotification()
    {
        await Task.Delay(2000);
        Instagram.SetActive(false);
        purchase.SetActive(false);
        await Task.Delay(100);
        Color color = phone.GetComponent<SpriteRenderer>().color;
        color.r *= 2f; 
        color.g *= 2f; 
        color.b *= 2f; 
        phone.GetComponent<SpriteRenderer>().color = color;
        message.SetActive(true);
        await Task.Delay(4000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private async void PurchaseNotification()
    {
        await Task.Delay(2000);
        purchase.SetActive(true);
        notification.Play();
        Color color = Instagram.GetComponent<SpriteRenderer>().color;
        color.r *= 0.5f; // Reduce red component by half
        color.g *= 0.5f; // Reduce green component by half
        color.b *= 0.5f; // Reduce blue component by half
        Instagram.GetComponent<SpriteRenderer>().color = color;
        await Task.Delay(2000);
        purchase.GetComponent<Collider2D>().enabled = true;
    }
}
