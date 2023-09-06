using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockScene : MonoBehaviour
{
    // The first gameobject to rotate
    [SerializeField] private GameObject firstObject;
    
    // The second gameobject to rotate
    [SerializeField] private GameObject secondObject;
    
    // The ratio of the second object's rotation speed to the first object's rotation speed
    private float speedRatio = 12f;
    
    // A flag to indicate whether the mouse is pressed on the first object's collider
    private bool isPressed = false;
    
    // The angular velocity of the first object
    private float angularVelocity = 6f;
    
    //clock gameobject
    [SerializeField] private GameObject clock;
    
    // index for each picture
    private static int PictureIndex;
    
    //boolean for touching
    private static bool touch;
    
    // pictures animator
    [SerializeField] private Animator[] _picturesAnimator;
    
    // the Z of hour gameobejct
    [SerializeField] private Transform hour;

    [SerializeField] private float[] lower;
    [SerializeField] private float[] upper;
    
    // the collider of last picture
    [SerializeField] private Collider2D lastpic;
    
    //arrow
    [SerializeField] private GameObject arrow;


    private void Start()
    {
        PictureIndex = 0;
        touch = true;
    }

    void Update()
    {
        if (hour.eulerAngles.z > lower[PictureIndex] && hour.eulerAngles.z < upper[PictureIndex] && touch)
        {
            NextPicture();
            touch = false;

        }
        
        // Check if the mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Get the collider of the first object
            Collider2D collider = firstObject.GetComponent<Collider2D>();
            
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform a 2D raycast at the click position
            RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
            
            // Check if the mouse position overlaps with the collider
            if (hit.collider != null)
            {
                // Check if the hit object is the one you want to detect clicks on
                if (hit.collider == lastpic)
                {
                    lastpic.enabled = false;
                    NextScene();
                    
                }
            }
            if (collider.OverlapPoint(mousePosition))
            {
                // Set the flag to true
                isPressed = true;
                
                
            }
        }
        // Check if the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            // Set the flag to false
            isPressed = false;
        }
        // Check if the flag is true
        if (isPressed)
        {
            // Rotate the first object by the angular velocity in clockwise direction
            firstObject.transform.Rotate(Vector3.forward, -angularVelocity);
            // Rotate the second object by the angular velocity divided by the speed ratio in clockwise direction
            secondObject.transform.Rotate(Vector3.forward, -angularVelocity / speedRatio);
        }
    }

    private async void NextPicture()
    {
        if (PictureIndex < _picturesAnimator.Length - 1)
        {
            
            _picturesAnimator[PictureIndex].enabled = true;
            await Task.Delay(2000);
            PictureIndex++;
            touch = true;


        }
        else
        {
            clock.SetActive(false);
            _picturesAnimator[PictureIndex].enabled = true;
            await Task.Delay(2000);
            arrow.SetActive(true);
            lastpic.enabled = true;

        }
    }
    
    private async void NextScene()
    {
        await Task.Delay(1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
