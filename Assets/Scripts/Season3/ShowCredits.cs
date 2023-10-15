using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowCredits : MonoBehaviour
{
    // The text component that displays the credits
    public GameObject[] creditsText;

    private void Start()
    {
        StartCoroutine(Credits());
    }


    private IEnumerator Credits()
    {
        yield return new WaitForSeconds(22.5f);
        creditsText[0].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        creditsText[1].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        creditsText[2].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        creditsText[3].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        creditsText[4].SetActive(true);
        yield return new WaitForSeconds(5.5f);
        creditsText[5].SetActive(true);
        

    }
}
