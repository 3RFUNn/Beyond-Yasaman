using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSkipScene : MonoBehaviour
{
    [SerializeField] private int initialdelay;
    
    [SerializeField] private int delay;
    
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
     private IEnumerator SkipScene()
     {
         yield return new WaitForSeconds(initialdelay);
         
         animator.SetTrigger("Animation");
         
         yield return new WaitForSeconds(delay);

         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start()
    {
        
        StartCoroutine(SkipScene());
    }
    
}
