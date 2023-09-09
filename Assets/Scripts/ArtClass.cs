using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArtClass : MonoBehaviour
{
   [SerializeField] private GameObject[] conversation;

   private void Start()
   {
      StartCoroutine(ArtClassConversation());
   }

   private IEnumerator ArtClassConversation()
   {
      yield return new WaitForSeconds(2);
      conversation[0].SetActive(true);
      yield return new WaitForSeconds(2);
      conversation[1].SetActive(true);
      yield return new WaitForSeconds(5);
      conversation[0].SetActive(false);
      conversation[1].SetActive(false);
      yield return new WaitForSeconds(1f);
      conversation[2].SetActive(true);
      yield return new WaitForSeconds(2);
      conversation[3].SetActive(true);
      yield return new WaitForSeconds(5);
      conversation[2].SetActive(false);
      conversation[3].SetActive(false);
      yield return new WaitForSeconds(1f);
      conversation[4].SetActive(true);
      yield return new WaitForSeconds(2);
      conversation[5].SetActive(true);
      yield return new WaitForSeconds(5);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


   }
}
