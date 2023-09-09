using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtGallery : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Animator photo;
    
    // Start is called before the first frame update
    void Start()
    {
        TakePicture();
    }
    
    private async void TakePicture()
    {
        await Task.Delay(11000);
        photo.SetTrigger("Picture");
        _audioSource.Play();
        await Task.Delay(2000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}
