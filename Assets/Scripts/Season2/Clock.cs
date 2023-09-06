using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // The first gameobject to rotate
    [SerializeField] private GameObject minute;
    
    // The second gameobject to rotate
    [SerializeField] private GameObject hour;

    [SerializeField] private Animator camera;

    [SerializeField] private GameObject Desk;
    
    // The ratio of the second object's rotation speed to the first object's rotation speed
    private float speedRatio = 12f;
    
    // A flag for stopping clock
    private bool _stop = false;
    
    // The angular velocity of the first object
    private float angularVelocity = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(hour.transform.eulerAngles.z - 225f) < 0.2f)
        {
            _stop = true;
        }

        if (!_stop)
        {
            // Rotate the first object by the angular velocity in clockwise direction
            minute.transform.Rotate(Vector3.forward, -angularVelocity);

            // Rotate the second object by the angular velocity divided by the speed ratio in clockwise direction
            hour.transform.Rotate(Vector3.forward, -angularVelocity / speedRatio);

            Color color = Desk.GetComponent<SpriteRenderer>().color;
            color.a -= 0.006f;
            Desk.GetComponent<SpriteRenderer>().color = color;
        }

        if (_stop)
        {
            camera.SetTrigger("Zoom");
        }
    }
}
