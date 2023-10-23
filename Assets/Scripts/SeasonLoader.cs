using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeasonLoader : MonoBehaviour
{
    [SerializeField] private Collider2D Next;
    [SerializeField] private Animator intro;

    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(Loader());
    }

    private IEnumerator Loader()
    {
        yield return new WaitForSeconds(10);
        intro.SetTrigger("Load");
        yield return new WaitForSeconds(0.5f);
        Next.enabled = true;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the hit collider is this one
            if (hit.collider != null && hit.collider == Next)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);



            }
        }
    }
}
