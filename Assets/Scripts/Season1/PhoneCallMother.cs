using System.Collections;

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneCallMother : MonoBehaviour
{

    // Use [SerializeField] private instead of public for the variables
    // This will make them private but still visible in the inspector
    [SerializeField] private GameObject[] callLogo; // The call logo on the phone
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject motherChild; // The picture of the mother and child
    [SerializeField] private GameObject[] textMother; // The text that mother says
    [SerializeField] private GameObject[] options; // The array of options for the response
    [SerializeField] private GameObject[] responses; // The array of responses for the selected option
    [SerializeField] private Animator animator; // The animator component of the phone

    private int conversationIndex; // The index of the current conversation
    private bool isConversationActive; // The flag to indicate if the conversation is active
    private bool isOptionSelected; // The flag to indicate if an option is selected
    private static int start = 0;
    private static int end = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the variables
        conversationIndex = 0;
        isConversationActive = false;
        isOptionSelected = false;

        // Hide the game objects that are not needed at the start
        motherChild.SetActive(false);
        foreach (GameObject option in options)
        {
            option.SetActive(false);
        }
        foreach (GameObject response in responses)
        {
            response.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the user clicks on the call logo collider
        if (Input.GetMouseButtonDown(0) && !isConversationActive)
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the collider component of the call logo
            Collider2D collider = callLogo[0].GetComponent<Collider2D>();

            // Check if the mouse position overlaps with the collider bounds
            if (collider.OverlapPoint(mousePos))
            {
                // Start the conversation
                StartCoroutine(StartConversation());
            }
        }

        // Check if the user clicks on an option collider when the conversation is active and no option is selected
        if (Input.GetMouseButtonDown(0) && isConversationActive && !isOptionSelected)
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Loop through the options array
            for (int i = 0; i < options.Length; i++)
            {
                // Get the collider component of the current option
                Collider2D collider = options[i].GetComponent<Collider2D>();

                // Check if the mouse position overlaps with the collider bounds
                if (collider.OverlapPoint(mousePos))
                {
                    // Select the option and show the corresponding response
                    SelectOption(i);
                    break;
                }
            }
        }
    }

    // A coroutine to start the conversation after clicking on the call logo
    IEnumerator StartConversation()
    {
        // Set the conversation flag to true
        isConversationActive = true;
        callLogo[1].SetActive(true);

        // Play the animation for 3 seconds
        animator.SetTrigger("Call");
        _audioSource.Play();
        yield return new WaitForSeconds(2f);
        _audioSource.Play();
        yield return new WaitForSeconds(2f);

        // Hide the call logo and show the mother and child picture
        callLogo[0].SetActive(false);
        callLogo[1].SetActive(false);
        motherChild.SetActive(true);

        // Show the first text that mother says and the first pair of options
        ShowTextAndOptions();
    }

    // A method to show the text that mother says and the pair of options based on the conversation index
    private async void ShowTextAndOptions()
    {
        await Task.Delay(1000);
        // Show the text that mother says based on the conversation index
        textMother[conversationIndex].SetActive(true);

        await Task.Delay(2500);

        // Show the pair of options based on the conversation index
        for (int i = start; i < end; i++)
        {
            options[i].SetActive(true);
            
            // Move the options to a specific position based on their index
            // You can adjust the position values according to your preference
            if (i == start)
            {
                options[i].transform.position = new Vector3(0f, -3.5f, 0f);
            }
            else
            {
                options[i].transform.position = new Vector3(0f, -4.5f, 0f);
            }
            
        }
    }

    // A method to select an option and show the corresponding response based on the option index
    void SelectOption(int optionIndex)
    {
        // Set the option flag to true
        isOptionSelected = true;

        // Hide all other options except for the selected one 
        for (int i = start; i < end; i++)
        {
            if (i != optionIndex)
            {
                options[i].SetActive(false);
            }
        }

        // Show the response based on the option index
        responses[optionIndex].SetActive(true);
        responses[optionIndex].GetComponent<Animator>().SetTrigger("Stop");

        // Move the response to a specific position based on the option index
        // You can adjust the position values according to your preference
        if (optionIndex == start)
        {
            responses[optionIndex].transform.position = new Vector3(0f, -3f, 0f);
        }
        else
        {
            responses[optionIndex].transform.position = new Vector3(0f, -3f, 0f);
        }

        // Wait for 3 seconds and then continue the conversation
        StartCoroutine(WaitAndContinue(3.5f));
    }

    // A coroutine to wait for a certain amount of time and then continue the conversation
    IEnumerator WaitAndContinue(float waitTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);
        textMother[conversationIndex].SetActive(false);

        // Hide the response that was shown
        foreach (GameObject response in responses)
        {
            response.SetActive(false);
        }

        // Increment the conversation index
        conversationIndex++;

        // Check if the conversation index is less than 3
        if (conversationIndex <= 2)
        {
            start += 2;
            end += 2;
            // Show the next text that mother says and the next pair of options
            ShowTextAndOptions();

            // Set the option flag to false
            isOptionSelected = false;
        }
        else
        {
            // End the conversation after 5 seconds
            StartCoroutine(EndConversation(2f));
        }
    }

    // A coroutine to end the conversation after a certain amount of time
    IEnumerator EndConversation(float waitTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        
        

        // You can add any other logic here to transition to another scene or do something else
    }
}

