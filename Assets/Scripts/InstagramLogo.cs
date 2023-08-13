using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstagramLogo : MonoBehaviour
{
    // This function is called every frame
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hit a collider
            if (hit.collider != null)
            {
                // Check if the hit collider is this one
                if (hit.collider == GetComponent<Collider2D>())
                {
                    // Call your custom function here
                    DoSomething();
                }
            }
        }
    }

    // Your custom function
    void DoSomething()
    {
        Debug.Log("You clicked on " + gameObject.name);
    }
}

