using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BedSleep : MonoBehaviour
{
    [SerializeField] private GameObject _phonebed;
    [SerializeField] private GameObject phone;

    [SerializeField] private GameObject momchat;

    [SerializeField] private GameObject yourchat;
    // Start is called before the first frame update
    void Start()
    {
        PhoneBed();
        
    }

    private async void PhoneBed()
    {
        await Task.Delay(3000);
        _phonebed.SetActive(false);
        phone.SetActive(true);
        await Task.Delay(2000);
        momchat.SetActive(true);
        await Task.Delay(3000);
        yourchat.SetActive(true);
        



    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
