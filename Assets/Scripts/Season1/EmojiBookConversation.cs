using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmojiBookConversation : MonoBehaviour
{
    // The book game object
    [SerializeField] private GameObject book;

    // The pen game object
    [SerializeField] private GameObject pen;

    // The array of choices game objects
    [SerializeField] private GameObject[] choices;

    // The array of book dialogues game objects
    [SerializeField] private GameObject[] bookDialogues;

    // The array of character dialogues game objects
    [SerializeField] private GameObject[] characterDialogues;

    private float delay;
    
    // last dialogue
    [SerializeField] private GameObject last;
    

    // Private variables
    private int dialogueIndex; // The current dialogue index
    private bool isChoiceActive; // Whether the choice is active or not
    private bool isPenActive; // Whether the pen is active or not
    
    [SerializeField] private Animator penAnimator; // The animator component of the pen
    // [SerializeField] private AudioSource audioSource; // The audio source component of this game object

    void Start()
    {
        // Initialize the variables
        dialogueIndex = 0;
        isChoiceActive = true;
        isPenActive = false;
        delay = 4f;

        // Activate the choices and deactivate the dialogues and the pen
        foreach (GameObject choice in choices)
        {
            choice.SetActive(true);
        }

        foreach (GameObject dialogue in bookDialogues)
        {
            dialogue.SetActive(false);
        }

        foreach (GameObject dialogue in characterDialogues)
        {
            dialogue.SetActive(false);
        }

        pen.SetActive(false);
    }

    void Update()
    {
        // Check if the user clicks on a choice
        if (isChoiceActive && Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Loop through the choices and check if any of them is clicked
            for (int i = 0; i < choices.Length; i++)
            {
                GameObject choice = choices[i];
                choice.SetActive(true);

                // Get the collider component of the choice
                Collider2D collider = choice.GetComponent<Collider2D>();

                // Check if the collider contains the mouse position
                if (collider.OverlapPoint(mousePosition))
                {
                    // Deactivate the other choices and move the clicked one to a new position
                    for (int j = 0; j < choices.Length; j++)
                    {
                        if (j != i)
                        {
                            choices[j].SetActive(false);
                        }
                        else
                        {
                            choice.transform.position = new Vector2(-0.1f, -0.5f); // Change this to your desired position
                            Option(choice);
                        }
                    }

                    // Set the choice flag to false and start the dialogue coroutine
                    isChoiceActive = false;
                    StartCoroutine(DialogueCoroutine());
                   
                    {
                        
                    }
                    break;
                }
            }
        }
        
        // Check if the user clicks on the pen
        if (isPenActive && Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the collider component of the pen
            Collider2D collider = pen.GetComponent<Collider2D>();

            // Check if the collider contains the mouse position
            if (collider.OverlapPoint(mousePosition))
            {
                // Play the second animation and the sound on the pen and deactivate its collider
                
                // penAnimator.Play("Pen");
                pen.SetActive(false);
                penAnimator.Play("Still");
                // audioSource.Play();
                collider.enabled = false;

                // Set the pen flag to false and start the dialogue coroutine
                isPenActive = false;
                

            }
        }
    }

    private async void Option(GameObject option)
    {
        await Task.Delay(3000);
        option.SetActive(false);
        
    }

    IEnumerator DialogueCoroutine()
    {
        if (dialogueIndex == 0)
        {
            yield return new WaitForSeconds(4);
            bookDialogues[dialogueIndex].SetActive(true);
        }
        // Check if there are more dialogues to show
        if (dialogueIndex < bookDialogues.Length  && dialogueIndex < characterDialogues.Length )
        {
            // Activate the book dialogue and wait for 4 seconds
            
            yield return new WaitForSeconds(2f);

            // Deactivate the book dialogue and activate the pen with its first animation and collider
            pen.SetActive(true);
            penAnimator.Play("Glow");
            pen.GetComponent<Collider2D>().enabled = true;

            // Set the pen flag to true and wait for user input
            isPenActive = true;
            yield return new WaitUntil(() => !isPenActive);
            
            pen.GetComponent<Collider2D>().enabled = false;

            // Wait for n seconds after user input and activate the character dialogue 
            yield return new WaitForSeconds(1f);
            characterDialogues[dialogueIndex].SetActive(true);

            // Increment the dialogue index and wait for 4 seconds
            dialogueIndex++;
            yield return new WaitForSeconds(6f);
            
            // Deactivate the character dialogue and start the dialogue coroutine again
            if (dialogueIndex < characterDialogues.Length)
            {
                characterDialogues[dialogueIndex - 1].SetActive(false);
                bookDialogues[dialogueIndex - 1].SetActive(false);
            }

            yield return new WaitForSeconds(1f);
            if (dialogueIndex == 6)
            {
                characterDialogues[5].SetActive(false);
            }
            bookDialogues[dialogueIndex].SetActive(true);

            StartCoroutine(DialogueCoroutine());
        }
        else
        {
            
            yield return new WaitForSeconds(4);
            // Load the next scene when the dialogues are finished
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
