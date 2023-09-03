using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PianoGame : MonoBehaviour
{
    //music notes picture
    [SerializeField] private GameObject music;
    
    [SerializeField] private GameObject _text;

    // The array of 2D colliders for the five keys to click
    [SerializeField] private Collider2D[] keys;

    // The array of audio clips for the five keys
    [SerializeField] private AudioClip[] sounds;

    // The audio source to play the sounds
    [SerializeField] private AudioSource audioSource;

    // The audio clip for the main music
    [SerializeField] private AudioClip mainMusic;

    // The array of game objects for the arrows
    [SerializeField] private GameObject[] arrows;

    // The index of the current key to click
    private int currentKey = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
        // Hide all the keys and arrows except the first one
        for (int i = 1; i < keys.Length; i++)
        {
            keys[i].gameObject.SetActive(false);
            arrows[i].SetActive(false);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse is over the current key
            if (keys[currentKey].OverlapPoint(mousePos))
            {
                if (currentKey == 0)
                {
                    _text.SetActive(false);
                }
                // Play the sound of the key
                audioSource.PlayOneShot(sounds[currentKey]);

                // Hide the current key and arrow
                keys[currentKey].gameObject.SetActive(false);
                arrows[currentKey].SetActive(false);

                // Increment the current key index
                currentKey++;

                // Check if all the keys have been clicked
                if (currentKey == keys.Length)
                {
                    music.SetActive(true);
                    
                    // Invoke the PlayMainMusic method after a delay
                    PlayMainMusic();
                    
                }
                else
                {
                    // Show the next key and arrow
                    keys[currentKey].gameObject.SetActive(true);
                    arrows[currentKey].SetActive(true);
                }
            }
        }
    }

    // Play the main music
    void PlayMainMusic()
    {
        // Play the main music clip
        audioSource.PlayOneShot(mainMusic);
        // Invoke the LoadNextScene method after the main music has finished playing
        Invoke("LoadNextScene", 15f);
    }
    
    // Load the next scene
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
