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
    [SerializeField] private GameObject Pencil;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] dialogue;
    [SerializeField] private GameObject[] bookdialogue;
    
    
    void Start()
    {
       

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
                Pencil.SetActive(false);
                _audioSource.Play();
                Pen.enabled = false;
                dialogue[0].SetActive(true);
                await Task.Delay(2500);
                bookdialogue[0].SetActive(true);
                await Task.Delay(2500);
                bookdialogue[1].SetActive(true);
                await Task.Delay(2000);
                _eventIndex++;
                Pencil.SetActive(true);
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
                Pencil.SetActive(false);
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
                Pencil.SetActive(true);
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
                Pencil.SetActive(false);
                dialogue[1].SetActive(false);
                bookdialogue[2].SetActive(false);
                await Task.Delay(1000);
                _audioSource.Play();
                dialogue[2].SetActive(true);
                await Task.Delay(2000);
                bookdialogue[3].SetActive(true);
                await Task.Delay(2000);
                _eventIndex++;
                Pencil.SetActive(true);
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
                Pencil.SetActive(false);
                _audioSource.Play();
                dialogue[3].SetActive(true);
                await Task.Delay(5500);
                dialogue[2].SetActive(false);
                bookdialogue[3].SetActive(false);
                dialogue[3].SetActive(false);
                await Task.Delay(1000);
                bookdialogue[4].SetActive(true);
                await Task.Delay(3000);
                Pencil.SetActive(true);
                Pen.enabled = true;
                _animator.speed = 1;
                _eventIndex++;


            }

        }

    }
    
    private async void Event3()
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
                Pencil.SetActive(false);
                _audioSource.Play();
                dialogue[4].SetActive(true);
                await Task.Delay(3500);
                bookdialogue[4].SetActive(false);
                dialogue[4].SetActive(false);
                await Task.Delay(1000);
                bookdialogue[5].SetActive(true);
                Pencil.SetActive(true);
                Pen.enabled = true;
                _animator.speed = 1;
                _eventIndex++;
                


            }

        }
    }
    private async void PenActive3()
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
                Pencil.SetActive(false);
                _audioSource.Play();
                dialogue[5].SetActive(true);
                await Task.Delay(5500);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                


            }

        }

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
            case 4 : Event3();
                break;
            case 5 : PenActive3();
                break;


               

        }
        
        

    }
}
