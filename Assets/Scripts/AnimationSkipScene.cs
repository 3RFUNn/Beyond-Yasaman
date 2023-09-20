using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSkipScene : MonoBehaviour
{
    [SerializeField] private int wait;

    [SerializeField] private Animator[] animator;
    // Start is called before the first frame update
    public async void _SkipScene()
    {
        await Task.Delay(wait + animator.Length);
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
