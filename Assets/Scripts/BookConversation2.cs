using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BookConversation2 : MonoBehaviour
{
    public static int _eventIndex = 0;
    [SerializeField] private Collider2D Pen;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] dialogue;
    [SerializeField] private GameObject[] bookdialogue;
    
    
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("S1_Scene22"))
        {
            _eventIndex = 4;
        }
    }
    
    
    private async void Event1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the hit collider is this one
            if (hit.collider != null && hit.collider == Pen)
            {
                
                _animator.speed = 0f;
                _audioSource.Play();
                Pen.enabled = false;
                dialogue[0].SetActive(true);
                await Task.Delay(2500);
                bookdialogue[0].SetActive(true);
                await Task.Delay(2500);
                bookdialogue[1].SetActive(true);
                await Task.Delay(2000);
                _eventIndex++;
                Pen.enabled = true;
                _animator.speed = 1;
                


            }

        }
    }
    private async void PenActive1()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the hit collider is this one
            if (hit.collider != null && hit.collider == Pen)
            {
                _animator.speed = 0f;
                Pen.enabled = false;
                dialogue[0].SetActive(false);
                bookdialogue[0].SetActive(false);
                bookdialogue[1].SetActive(false);
                await Task.Delay(1000);
                _audioSource.Play();
                dialogue[1].SetActive(true);
                await Task.Delay(2000);
                bookdialogue[2].SetActive(true);
                await Task.Delay(2000);
                _eventIndex++;
                Pen.enabled = true;
                _animator.speed = 1;

            }

        }
    }
    
    private async void Event2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the hit collider is this one
            if (hit.collider != null && hit.collider == Pen)
            {
                _animator.speed = 0f;
                Pen.enabled = false;
                dialogue[1].SetActive(false);
                bookdialogue[2].SetActive(false);
                await Task.Delay(1000);
                _audioSource.Play();
                dialogue[2].SetActive(true);
                await Task.Delay(2000);
                bookdialogue[3].SetActive(true);
                await Task.Delay(2000);
                _eventIndex++;
                Pen.enabled = true;
                _animator.speed = 1;
                
            }

        }
    }

    private async void PenActive2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the hit collider is this one
            if (hit.collider != null && hit.collider == Pen)
            {
                _animator.speed = 0f;
                Pen.enabled = false;
                _audioSource.Play();
                dialogue[3].SetActive(true);
                await Task.Delay(3000);
                dialogue[2].SetActive(false);
                bookdialogue[3].SetActive(false);
                dialogue[3].SetActive(false);
                await Task.Delay(2000);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }

        }

    }

    private async void Event3()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_eventIndex)
        {
            case 0 : Event1();
                break;
            case 1 : PenActive1();
                break;
            case 2 : Event2();
                break;
            case 3 : PenActive2();
                break;
            
            

        }
        
        

    }
}
