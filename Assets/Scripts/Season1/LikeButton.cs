using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LikeButton : MonoBehaviour
{
    [SerializeField] private Collider2D[] like;
    [SerializeField] private Animator[] _animators;
    private static int number = 0;
    

    void Update()
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
                if (hit.collider == like[number])
                {
                    if (number == 3)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        // Call your custom function here

                        _animators[number].SetTrigger("Like");
                        StartCoroutine(Next());
                    }


                }
                

            }
        }
        

    }

    private IEnumerator Next()
    {
        yield return new WaitForSeconds(1);
        like[number].enabled = false;
        if (number + 1 < like.Length)
        {
            number++;
        }
        like[number].enabled = true;
    }
}
