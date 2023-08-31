using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaptopController : MonoBehaviour
{
    // The game objects for the power button, power on and power off laptop
    [SerializeField] private GameObject powerButton;
    [SerializeField] private GameObject powerOnLaptop;
    [SerializeField] private GameObject powerOffLaptop;

    // The audio sources for the power on and power off sounds
    [SerializeField] private AudioSource powerOnSound;
    [SerializeField] private AudioSource powerOffSound;

    // The animator for the power button animation
    [SerializeField] private Animator powerButtonAnimator;

    // The number of times the laptop will turn on and off
    private int loopCount = 3;
    
    private bool PowerOff = false;

    // The time interval between the laptop turning on and off
    private float interval = 3f;

    // A variable to keep track of the current loop
    private int currentLoop = 0;

    // A variable to indicate if the laptop is on or off
    private bool isOn = false;

    // A variable to store a reference to the coroutine that turns the laptop on and off
    private Coroutine turnOnOffCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial state of the game objects
        powerButton.SetActive(true);
        powerOnLaptop.SetActive(false);
        powerOffLaptop.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the user clicks on the power button
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the collider at the mouse position
            Collider2D collider = Physics2D.OverlapPoint(mousePosition);

            // Check if the collider is the power button
            if (collider != null && collider.gameObject == powerButton)
            {
                if (!PowerOff)
                {
                    // Play the power button animation
                    powerButtonAnimator.Play("Power");
                    powerButton.GetComponent<CircleCollider2D>().enabled = false;
                    // Turn on the laptop
                    TurnOn();
                }
                else
                {
                    powerButtonAnimator.Play("Power");

                }
            }
        }
    }

    // A method to turn on the laptop
    private async void TurnOn()
    {
        await Task.Delay(1000);
        // Check if the laptop is already on or the loop count is exceeded
        if (currentLoop >= loopCount)
        {
            turnOnOffCoroutine = StartCoroutine(TurnOffAfterDelay(interval));
        }

        // Increment the current loop count
        currentLoop++;

        // Set the laptop state to on
        isOn = true;

        // Play the power on sound
        powerOnSound.Play();

        // Activate the power on laptop game object and deactivate the others
        powerOnLaptop.SetActive(true);
        powerOffLaptop.SetActive(false);
        powerButton.SetActive(false);

        // Start a coroutine to turn off the laptop after a delay
        turnOnOffCoroutine = StartCoroutine(TurnOffAfterDelay(interval));
    }

    // A method to turn off the laptop after a delay
    IEnumerator TurnOffAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Turn off the laptop
        TurnOff();
    }

    // A method to turn off the laptop
    private async void TurnOff()
    {
        
        // Set the laptop state to off
        isOn = false;

        // Play the power off sound
        powerOffSound.Play();

        // Activate the power off laptop game object and deactivate the others
        powerOffLaptop.SetActive(true);
        powerOnLaptop.SetActive(false);

        if (currentLoop < loopCount)
        {
            
            turnOnOffCoroutine = StartCoroutine(ActivatePowerButtonAfterDelay(1f));
        }
        else
        {
            PowerOff = true;
            powerButton.SetActive(true);
            powerButton.GetComponent<CircleCollider2D>().enabled = true;
            await Task.Delay(4000);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        
        
    }

    // A method to activate the power button after a delay of one second
    IEnumerator ActivatePowerButtonAfterDelay(float delay)
    {
        // Wait for one second
        yield return new WaitForSeconds(delay);

        // Activate the power button game object and deactivate the others
        powerButton.SetActive(true);
        powerButton.GetComponent<CircleCollider2D>().enabled = true;
        
        
        
        
    }
}

