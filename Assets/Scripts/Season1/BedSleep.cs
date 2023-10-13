using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedSleep : MonoBehaviour
{
    [SerializeField] private GameObject _phonebed;
    [SerializeField] private GameObject phone;

    [SerializeField] private GameObject momchat;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject[] yourchat;

    private static bool trigger,trigger2 = false; 
    // Start is called before the first frame update
    void Start()
    {
        
        PhoneBed();
        
    }

    private async void PhoneBed()
    {
        await Task.Delay(3000);
        _audioSource.Play();
        await Task.Delay(500);
        _phonebed.SetActive(false);
        phone.SetActive(true);
        await Task.Delay(500);
        momchat.SetActive(true);
        await Task.Delay(2000);
        yourchat[0].SetActive(true);
        yourchat[1].SetActive(true);
        yourchat[2].SetActive(true);
        trigger = true;
    }

    private async void PhoneBed2()
    {
        await Task.Delay(3000);
        phone.SetActive(false);
        _phonebed.SetActive(true);
        book.SetActive(true);
        await Task.Delay(2000);
        book.GetComponent<Animator>().enabled = true;
        await Task.Delay(1000);
        book.GetComponent<Collider2D>().enabled = true;
        trigger2 = true;


    }

    private void CheckAnswer()
    {
        foreach (var option in yourchat)
        {
            // If this option has been clicked, show it as chosen and hide others 
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in world coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // If the ray hit something, check if it was this option
                if (hit.collider != null && hit.collider == option.GetComponent<Collider2D>())
                {
                    // Set the optionChosen flag to true
                    option.gameObject.transform.position = new Vector3(0.65f, 0.7f, 0f);
                    option.gameObject.GetComponent<Animator>().SetTrigger("Stop");

                    foreach (GameObject otherOption in yourchat)
                    {
                        if (otherOption != option)
                        {
                            otherOption.gameObject.SetActive(false);
                        }
                            
                    }

                    trigger = false;
                    PhoneBed2();
                    
                }
            }
        }
    }

    private void touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hit a collider
            if (hit.collider != null)
            {
                // Check if the hit collider is this one
                if (hit.collider == book.GetComponent<Collider2D>())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(trigger)
            CheckAnswer();
        
        if (trigger2)
            touch();

    }
}
