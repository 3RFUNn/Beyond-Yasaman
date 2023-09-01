using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversityTalk : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] chats;
    void Start()
    {
        StartCoroutine(UniTalk());
    }

    private IEnumerator UniTalk()
    {
        yield return new WaitForSeconds(3);
        _animator.SetTrigger("Talk");
        yield return new WaitForSeconds(3);
        chats[1].SetActive(true);
        yield return new WaitForSeconds(2.5f);
        chats[0].SetActive(true);
        yield return new WaitForSeconds(5);
        chats[0].SetActive(false);
        yield return new WaitForSeconds(1);
        chats[2].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
    
}
