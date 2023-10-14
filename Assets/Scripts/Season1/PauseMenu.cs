using UnityEngine;
using UnityEngine.SceneManagement;

namespace Season1
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pause;
        // A boolean variable to store the pause state
        public bool isPaused = false;

        // A reference to the pause menu game object
        public GameObject pauseMenu;
        
        public static PauseMenu instance;

        void Awake()
        {
            // Check if the instance already exists
            if (instance == null)
            {
                // If not, assign the current object to it
                instance = this;
                // Call DontDestroyOnLoad to make the object persistent
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // If it does, destroy the current object to avoid duplicates
                Destroy(gameObject);
            }
        }
        
        // A method to toggle the pause state
        public void Pause()
        {
            // If the game is not paused, set isPaused to true and stop the time
            if (!isPaused)
            {
                isPaused = true;
                Time.timeScale = 0f;
            
            }
            // If the game is paused, set isPaused to false and resume the time
            else
            {
                isPaused = false;
                Time.timeScale = 1f;
            
            }

            // Enable or disable the pause menu accordingly
            pauseMenu.SetActive(isPaused);
        }

        // A method to resume the game
        public void Resume()
        {
            // Set isPaused to false and resume the time
            isPaused = false;
            Time.timeScale = 1f;

            // Disable the pause menu
            pauseMenu.SetActive(false);
        }

        // A method to load the main menu scene
        public void Menu()
        {
            // Resume the time
            Time.timeScale = 1f;
            
            // Load the main menu scene by its name or index
            SceneManager.LoadScene("MainMenu");
            pauseMenu.SetActive(false);
        }

        private void Update()
        {
            pause.SetActive(!SceneManager.GetActiveScene().name.Equals("MainMenu"));
        }
    }
}