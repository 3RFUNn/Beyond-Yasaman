using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BookConversation : MonoBehaviour
{
    
    // Assign these in the Inspector
    [SerializeField] private GameObject[] characterDialogues; // The character's dialogue gameobjects
    [SerializeField] private GameObject[] bookAnswers; // The book's answer gameobjects
    [SerializeField] private Animator penAnimator; // The pen's animator component
    [SerializeField] private Collider2D penCollider; // The pen's collider component
    [SerializeField] private AudioSource _audioSource;
    private int dialogueIndex; // The current index of the dialogue
    private bool isTalking; // A flag to indicate if the dialogue is in progress

    private void Start()
    {


        // Initialize the dialogue index and the talking flag
        dialogueIndex = 0;
        isTalking = false;
        penAnimator.Play("Pen");

        // Hide all the dialogue gameobjects at the start
        foreach (GameObject dialogue in characterDialogues)
        {
            dialogue.SetActive(false);
        }
        foreach (GameObject answer in bookAnswers)
        {
            answer.SetActive(false);
        }
    }

    private void Update()
    {
        // Check if the user clicks the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse position overlaps with the pen collider
            if (penCollider.OverlapPoint(mousePosition))
            {
                // If the dialogue is not in progress, start it
                if (!isTalking)
                {
                    StartDialogue();
                }
                // Otherwise, end it
                else
                {
                    EndDialogue();
                }
            }
        }
    }

    private void StartDialogue()
    {
        
            // Set the talking flag to true
            isTalking = true;
            _audioSource.Play();

            // Stop the pen animation and deactivate its collider
            penAnimator.speed = 0f;
            penCollider.enabled = false;

            // Show the character's dialogue gameobject at the current index
            characterDialogues[dialogueIndex].SetActive(true);

            // Invoke the ShowBookAnswer method after 2 seconds
            Invoke("ShowBookAnswer", 2f);
        
    }

    private void ShowBookAnswer()
    {
        // Check if there is a book answer at the current index
        if (dialogueIndex < bookAnswers.Length)
        {
            // Show the book's answer gameobject at the current index
            bookAnswers[dialogueIndex].SetActive(true);

            // Invoke the ResumePenAnimation method after 2 seconds
            Invoke("ResumePenAnimation", 2f);
        }
    }

    private void ResumePenAnimation()
    {
        // Resume the pen animation and activate its collider
        penAnimator.speed = 1f;
        penCollider.enabled = true;
    }

    private void EndDialogue()
    {
        // Set the talking flag to false
        isTalking = false;

        // Hide the character's dialogue gameobject at the current index
        if (dialogueIndex < 5)
        {
            characterDialogues[dialogueIndex].SetActive(false);

            // Check if there is a book answer at the current index
            if (dialogueIndex < bookAnswers.Length)
            {
                // Hide the book's answer gameobject at the current index
                if (dialogueIndex < 5)
                    bookAnswers[dialogueIndex].SetActive(false);

                // Increment the dialogue index by one
                dialogueIndex++;
                // Cancel any pending invokes from StartDialogue or ShowBookAnswer methods 
                CancelInvoke();

                // Resume the pen animation and activate its collider 
                ResumePenAnimation();
            }
        }
        else
        {
            characterDialogues[++dialogueIndex].SetActive(true);
            penAnimator.speed = 0f;
            _audioSource.Play();
            Invoke("LoadNextScene",4f);
        }

    }

    private void LoadNextScene()
    {
        // Load the next scene using SceneManager.LoadScene method
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

