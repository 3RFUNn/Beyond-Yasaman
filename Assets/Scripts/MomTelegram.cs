using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MomTelegram : MonoBehaviour
{
    // telegram logo
    [SerializeField] private GameObject logo;

    [SerializeField] private Animator _animator;
    
    // chat with mom
    [SerializeField] private GameObject _chat;

    [SerializeField] private GameObject _answer;

    [SerializeField] private Animator answerAnimator;

    [SerializeField] private AudioSource _audioSource;
    
    void Start()
    {
        
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
                if (hit.collider.gameObject == logo)
                {
                    _animator.SetTrigger("Logo");
                    logo.GetComponent<Collider2D>().enabled = false;
                    Chat();
                }

                if (hit.collider.gameObject == _answer)
                {
                    answerAnimator.speed = 0;
                    _answer.GetComponent<Collider2D>().enabled = false;
                    _answer.transform.position = new Vector3(-0.1f, -0.33f, 0f);
                    Color color = _answer.GetComponent<SpriteRenderer>().color;
                    color.a = 1f;
                    _answer.GetComponent<SpriteRenderer>().color = color;
                    NextScene();
                    
                }
            }
        }
        
    }

    private async void Chat()
    {
        await Task.Delay(2000);
        logo.SetActive(false);
        _audioSource.Play();
        await Task.Delay(3000);
        _chat.SetActive(true);
        await Task.Delay(3000);
        _answer.SetActive(true);
        await Task.Delay(1000);
        _answer.GetComponent<Collider2D>().enabled = true;
    }


    private async void NextScene()
    {
        await Task.Delay(5000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
