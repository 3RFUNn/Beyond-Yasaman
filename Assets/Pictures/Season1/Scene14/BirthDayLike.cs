using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirthDayLike : MonoBehaviour
{
    [SerializeField] private Collider2D[] colliders;

    [SerializeField] private Animator[] _animator;

    private int number;

    // Start is called before the first frame update
    void Start()
    {
        number = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(number);
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hit a collider
            if (hit.collider != null)
            {
                if (number == 3)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    hit.collider.enabled = false;
                    _animator[number].SetTrigger("Like");
                    Like();
                }


            }
        }
    }

    private async void Like()
    {
        await Task.Delay(1000);
        if (number + 1 < colliders.Length)
        {
            number++;
        }
        colliders[number].enabled = true;
    }

}

