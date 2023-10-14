using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaptopFix : MonoBehaviour
{

    [SerializeField] private GameObject LaptopCall;
    // Start is called before the first frame update
    void Start()
    {
        Laptop();
    }

    private async void Laptop()
    {
        await Task.Delay(12000);
        LaptopCall.SetActive(false);
        await Task.Delay(7000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
