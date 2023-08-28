using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClassCloud : MonoBehaviour
{

    // Assign these in the inspector
    [SerializeField] private List<GameObject> activeClouds;
    [SerializeField] private  GameObject lastCloud;
    [SerializeField] private  Camera mainCamera;
    [SerializeField] private  AudioSource audioSource;

    // The original size and position of the camera
    private float originalSize;
    private Vector3 originalPosition;

    // The zoomed size of the camera
    private float zoomedSize = 1f;

    // The duration of the zooming animation in seconds
    private float zoomDuration = 1f;

    // The duration of showing the picture and audio in seconds
    private float showDuration = 3f;

    // The number of clouds clicked
    private int _counter = 3;

    void Start()
    {
        // Get the original size and position of the camera
        originalSize = mainCamera.orthographicSize;
        originalPosition = mainCamera.transform.position;

        // Deactivate the last cloud at first
        lastCloud.SetActive(false);

        // Update the counter text
        

        // Add a Physics 2D Raycaster component to the main camera
        mainCamera.AddComponent<Physics2DRaycaster>();
    }

    public void ZoomAndShow(GameObject cloud)
    {
        Debug.Log(_counter);
        // Start the zooming and showing coroutine for this cloud
        StartCoroutine(ZoomAndShowCoroutine(cloud));
    }

    IEnumerator ZoomAndShowCoroutine(GameObject cloud)
    {
        // Get the sprite renderer and audio source components of this cloud
        GameObject gameObject = cloud.gameObject.transform.GetChild(0).gameObject;
        AudioSource cloudAudioSource = cloud.GetComponent<AudioSource>();
        Animator animator = cloud.GetComponent<Animator>();

        // Get the zoomed position of the camera based on this cloud's position
        Vector3 zoomedPosition = new Vector3(cloud.transform.position.x, cloud.transform.position.y, originalPosition.z);

        // Zoom in to this cloud
        yield return StartCoroutine(Zoom(originalSize, zoomedSize, originalPosition, zoomedPosition));

        // Show the picture and play the audio
        gameObject.SetActive(true);
        animator.enabled = false;
        audioSource.clip = cloudAudioSource.clip;
        audioSource.Play();
        

        // Wait for 3 seconds
        yield return new WaitForSeconds(showDuration);

        // Hide the picture and stop the audio
        gameObject.SetActive(false);
        audioSource.Stop();

        // Zoom out to the original view
        yield return StartCoroutine(Zoom(zoomedSize, originalSize, zoomedPosition, originalPosition));

        // Deactivate this cloud
        cloud.SetActive(false);

        // Remove this cloud from the active list
        activeClouds.Remove(cloud);

        // Increment the counter and update the counter text
        _counter--;
        

        // If there are no more active clouds, activate the last cloud
        if (_counter == 0)
        {
            lastCloud.SetActive(true);
        }

        if (_counter < 0)
        {
            Next();
        }
    }

    private async void Next()
    {
        await Task.Delay(3000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Zoom(float fromSize, float toSize, Vector3 fromPosition, Vector3 toPosition)
    {
        // Get the current time
        float startTime = Time.time;

        // Loop until the zooming animation is done
        while (Time.time - startTime < zoomDuration)
        {
            // Calculate how much of the animation has been completed
            float t = (Time.time - startTime) / zoomDuration;

            // Interpolate the size and position of the camera
            mainCamera.orthographicSize = Mathf.Lerp(fromSize, toSize, t);
            mainCamera.transform.position = Vector3.Lerp(fromPosition, toPosition, t);

            // Wait for the next frame
            yield return null;
        }

        // Set the final size and position of the camera
        mainCamera.orthographicSize = toSize;
        mainCamera.transform.position = toPosition;
    }

    void Update()
    {
        // If the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Cast the ray and get a RaycastHit2D object
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // If the ray hit a collider
            if (hit.collider != null)
            {
                // Get a reference to the game object that has the collider
                GameObject cloud = hit.collider.gameObject;

                // If this game object is one of the active clouds, and it is not deactivated yet
                if (cloud.activeSelf)
                {
                    // Call the zoom and show method for this game object
                    ZoomAndShow(cloud);
                    cloud.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
        }
    }
}

