using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelegramChat : MonoBehaviour
{
    [SerializeField] private GameObject logo;

    [SerializeField] private Collider2D logoCollider;

// Reference to the messenger app that is initially deactivated
    [SerializeField] private GameObject[] app;
    

// Reference to the welcome text that is initially hidden
    [SerializeField] private GameObject welcomeText;

// Reference to the chat bar that is initially hidden
    [SerializeField] private GameObject chatBar;

// Reference to the text message from the friend that is initially hidden
    [SerializeField] private GameObject friendMessage;

// Reference to the three options of text messages to choose from that are initially hidden
    [SerializeField] private GameObject[] options;

// Reference to the text message that shows the chosen option that is initially hidden
    

// Reference to the text messages from the friend that are replies to the chosen option that are initially hidden
    [SerializeField] private GameObject[] friendReplies;

// Reference to the last text message to send to the friend that is initially hidden
    [SerializeField] private GameObject lastMessage;

// A boolean flag to indicate if the logo has been clicked
    private bool logoClicked = false;

// A boolean flag to indicate if the chat has been clicked
    private bool chatClicked = false;

// A boolean flag to indicate if an option has been chosen
    private bool optionChosen = false;

// A boolean flag to indicate if the last message has been sent
    private bool lastMessageSent = false;

// A timer to control the timing of showing and hiding elements
    private float timer = 0f;


   

// Update is called once per frame
    void Update()
    {
        // If the logo has not been clicked, check for mouse input
        if (!logoClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in world coordinates
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                

                // Check if the mouse position overlaps with the pen collider
                if (logoCollider.OverlapPoint(mousePosition))
                {
                    // Set the logoClicked flag to true
                    logoClicked = true;

                    // Deactivate the logo
                    logo.SetActive(false);

                    // Activate the app
                    app[0].SetActive(false);
                    app[1].SetActive(true);
                    

                    // Show the welcome text
                    welcomeText.SetActive(true);
                    Delay(3000,welcomeText,chatBar);
                }
            }
            
        }
        else if (!chatClicked) // If the logo has been clicked but not the chat, check for mouse input
        {
            // If the left mouse button is pressed, check if it hits the chat bar
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in world coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // If the ray hit something, check if it was the chat bar
                if (hit.collider != null && hit.collider == chatBar.GetComponent<Collider2D>())
                {
                    // Set the chatClicked flag to true
                    chatClicked = true;

                    // Hide the welcome text
                    welcomeText.SetActive(false);
                    chatBar.SetActive(false);

                    // Show the friend message
                    friendMessage.SetActive(true);

                    // Show the options of text messages to choose from
                    foreach (GameObject option in options)
                    {
                        option.gameObject.SetActive(true);
                        
                    }
                }
            }
        }
        else if (!optionChosen) // If the chat has been clicked but not an option, check for mouse input
        {
            // Loop through each option and check if it has been clicked
            foreach (var option in options)
            {
                // If this option has been clicked, show it as chosen and hide others 
                if (Input.GetMouseButtonDown(0))
                {
                    // Get the mouse position in world coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    // If the ray hit something, check if it was this option
                    if (hit.collider != null && hit.collider == option.GetComponent<Collider2D>())
                    {
                        // Set the optionChosen flag to true
                        optionChosen = true;
                        option.gameObject.transform.position = new Vector3(1.7f, 0f, 0f);
                        

                        foreach (GameObject otherOption in options)
                        {
                            if (otherOption != option)
                            {
                                otherOption.gameObject.SetActive(false);
                            }
                            
                        }

                        // Break the loop
                        break;
                    }
                }
            }
        }
        else if (!lastMessageSent)        // If an option has been chosen but not the last message, update the timer and show the friend replies
        {
            // Increment the timer by the time since the last frame
            timer += Time.deltaTime;

            // If the timer is greater than a certain value, show the next friend reply
            if (timer > 2f && !friendReplies[0].activeSelf)
            {
                friendReplies[0].SetActive(true);
            }
            else if (timer > 4f && !friendReplies[1].activeSelf)
            {
                friendReplies[1].SetActive(true);
            }
            else if (timer > 6f && !lastMessage.gameObject.activeSelf)
            {
                lastMessage.gameObject.SetActive(true);
            }
            else if (timer > 8f)
            {
                lastMessage.GetComponent<Collider2D>().enabled = true;
            }

            // If the last message has been clicked, show it as sent and end the scene
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in world coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                // If the ray hit something, check if it was the last message
                if (hit.collider != null && hit.collider == lastMessage.GetComponent<Collider2D>())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                }
            }
        }            
    }

    private async void Delay(int time,params GameObject[] gameObject)
    {
        await Task.Delay(time);
        gameObject[0].SetActive(false);
        gameObject[1].SetActive(true);
    }
}