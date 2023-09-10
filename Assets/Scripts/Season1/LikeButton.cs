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
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            // Check if the mouse position overlaps with the pen collider
            if (like[number].OverlapPoint(mousePosition))
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
