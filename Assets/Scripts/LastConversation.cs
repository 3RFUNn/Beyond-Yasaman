using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastConversation : MonoBehaviour
{
    // The pen game object with the glowing and movement animations
    [SerializeField] private GameObject pen;

    
    [SerializeField] private AudioSource _audioSource;

    // The arrays of dialogue game objects for the player and the book
    [SerializeField] private GameObject[] playerDialogues;
    [SerializeField] private GameObject[] bookDialogues;
    

    // The index of the current dialogue
    private int dialogueIndex;

    // The number of dialogues
    private int dialogueCount;

    // The delay between dialogues in seconds
    [SerializeField] private float dialogueDelay = 3f;

    [SerializeField] private float firstDelay;

    
    

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the dialogue index and count
        dialogueIndex = 7;
        dialogueCount = Mathf.Min(playerDialogues.Length, bookDialogues.Length);

        // Hide all the dialogue game objects
        foreach (GameObject dialogue in playerDialogues)
        {
            dialogue.SetActive(false);
        }
        foreach (GameObject dialogue in bookDialogues)
        {
            dialogue.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the user clicks on the pen collider
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the pen collider component
            Collider2D penCollider = pen.GetComponent<Collider2D>();

            // Check if the mouse position overlaps with the pen collider
            if (penCollider.OverlapPoint(mousePosition))
            {
                _audioSource.Play();
                penCollider.enabled = false;
                // Stop the glowing animation and play the movement animation
                pen.GetComponent<Animator>().SetBool("Glow", false);
                pen.GetComponent<Animator>().SetBool("Write", true);

                // Start the dialogue coroutine
                StartCoroutine(DialogueCoroutine());
            }
        }
    }

    // A coroutine that handles the dialogue sequence
    private IEnumerator DialogueCoroutine()
    {
        
        // Loop through the dialogues
        if (dialogueIndex < dialogueCount)
        {
            // Show the current player dialogue and hide the previous one
            if (dialogueIndex > 0)
            {
                playerDialogues[dialogueIndex - 1].SetActive(false);
            }

            // initial delay of player dialogue
            yield return new WaitForSeconds(firstDelay);
            
            playerDialogues[dialogueIndex].SetActive(true);

            // Wait for the dialogue delay
            yield return new WaitForSeconds(dialogueDelay);

            // Show the current book dialogue and hide the previous one
            if (dialogueIndex > 0)
            {
                bookDialogues[dialogueIndex - 1].SetActive(false);
            }

            bookDialogues[dialogueIndex].SetActive(true);

            if (bookDialogues[dialogueIndex].tag.Equals("Change"))
            {
                yield return new WaitForSeconds(3);
                bookDialogues[dialogueIndex].transform.GetChild(0).gameObject.SetActive(true);
            }

            // Wait for the dialogue delay
            yield return new WaitForSeconds(dialogueDelay);

            // Increment the dialogue index
            dialogueIndex++;


            // Stop the movement animation and start the glowing animation
            pen.GetComponent<Animator>().SetBool("Glow", true);
            pen.GetComponent<Animator>().SetBool("Write", false);
            pen.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            playerDialogues[dialogueIndex].SetActive(true);

            // Wait for the dialogue delay
            yield return new WaitForSeconds(5);
            
            // Call the next scene function
            NextScene();
        }

    }
    

    // A function that loads the next scene
    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
