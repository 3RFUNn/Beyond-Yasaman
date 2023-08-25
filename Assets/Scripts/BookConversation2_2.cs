using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BookConversation2_2 : MonoBehaviour
{
   // The game objects that contain the dialogue texts
    public GameObject Dialogue1;
    public GameObject Dialogue2;
    public GameObject Dialogue3;
    public GameObject Dialogue4;

    // The game object that contains the pen
    public GameObject Pen;
    [SerializeField] private Collider2D pen;
    [SerializeField] private AudioSource _audioSource;
    private bool write = false;
    
    

    // The index of the current dialogue to show
    private int dialogueIndex;

    // The delay between dialogues in seconds
    private float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the animator component of the pen

        // Set the dialogue index to 0
        dialogueIndex = 1;
        Pen.GetComponent<Animator>().speed = 0f;
        book();


    }

    private async void book()
    {
        await Task.Delay(2000);
        Dialogue1.SetActive(true);
        write = true;
        Pen.GetComponent<Animator>().speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (write)
        {
            // Check if the user clicks on the pen
            if (Input.GetMouseButtonDown(0))
            {
                // Get the mouse position in world coordinates
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Get the collider2D component of the pen
                Collider2D penCollider = Pen.GetComponent<Collider2D>();

                // Check if the mouse position overlaps with the pen collider
                if (penCollider.OverlapPoint(mousePosition))
                {
                    // Show the next dialogue
                    ShowNextDialogue();
                    pen.enabled = false;
                    Pen.GetComponent<Animator>().speed = 0f;
                }
            }
        }
    }

    // A method to hide all the dialogues
    

    // A method to show the next dialogue
    void ShowNextDialogue()
    {
        // Increment the dialogue index by 1
        dialogueIndex++;
        // Check if the dialogue index is within range
        if (dialogueIndex > 0 && dialogueIndex <= 4)
        {
            // Start a coroutine to show the dialogue with a delay
            StartCoroutine(ShowDialogueWithDelay(dialogueIndex));
        }
    }

    // A coroutine to show a dialogue with a delay
    IEnumerator ShowDialogueWithDelay(int index)
    {
        _audioSource.Play();
        // Get the game object that corresponds to the index
        GameObject dialogue = null;
        switch (index)
        {
            
            case 2:
                dialogue = Dialogue2;
                break;
            case 4:
                dialogue = Dialogue4;
                break;

        }

        // Check if the dialogue is not null
        if (dialogue != null)
        {
            // Set the active state of the dialogue to true
            dialogue.SetActive(true);
        }

        Debug.Log("Hi");
        yield return new WaitForSeconds(3);
        Dialogue3.SetActive(true);
        dialogueIndex++;
        if (dialogueIndex < 5)
        {
            pen.enabled = true;
            Pen.GetComponent<Animator>().speed = 1f;
        }
        else
        {
            write = false;
            LoadNext();
        }

    }

    private async void LoadNext()
    {
        await Task.Delay(2000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
