using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    [SerializeField] private int wait;
    // Start is called before the first frame update
    public async void _SkipScene()
    {
        await Task.Delay(wait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start()
    {
        _SkipScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
