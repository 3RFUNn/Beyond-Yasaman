using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MobileOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        PhoneWake();
    }

    private async void PhoneWake()
    {
        await Task.Delay(2000);
        _audioSource.Play();
        GameObject child = this.transform.GetChild(0).gameObject;
        child.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
