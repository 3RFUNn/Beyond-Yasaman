using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeskBookGlow : MonoBehaviour
{
    // animator of the book
    [SerializeField] private Animator _animator;
    // collider of the book
    [SerializeField] private Collider2D _book;

    [SerializeField] private Animator _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BookGlow());

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
                if (hit.collider == _book)
                {
                    _book.enabled = false;
                    
                }
            }
        }
        
    }

    private IEnumerator BookGlow()
    {
        yield return new WaitForSeconds(1);
        _camera.SetTrigger("Animation");
        yield return new WaitForSeconds(2);
        _animator.enabled = true;
        _book.enabled = true;
        yield return new WaitUntil(() => !_book.enabled);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
