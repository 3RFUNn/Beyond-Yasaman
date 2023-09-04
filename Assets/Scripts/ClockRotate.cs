using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotate : MonoBehaviour
{
    // The second game object that rotates with this one
    public GameObject otherObject;

    // The previous mouse position in world coordinates
    private Vector3 previousMousePosition;

    // The angle of rotation in degrees
    private float angle;

    void Start()
    {
        // Initialize the previous mouse position
        previousMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Get the current mouse position in world coordinates
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the vector from the previous mouse position to the current one
            Vector3 mouseDelta = currentMousePosition - previousMousePosition;

            // Get the vector from the object's position to the current mouse position
            Vector3 mouseDirection = currentMousePosition - transform.position;

            // Calculate the angle between the two vectors using the cross product
            float crossZ = Vector3.Cross(mouseDelta, mouseDirection).z;

            // Calculate the delta angle using the arc tangent function
            float deltaAngle = Mathf.Rad2Deg * Mathf.Atan2(crossZ, Vector3.Dot(mouseDelta, mouseDirection));

            // Update the angle of rotation
            angle += deltaAngle;

            // Rotate this object by the delta angle around its z-axis
            transform.Rotate(0f, 0f, deltaAngle);

            // Rotate the other object by a fraction of the delta angle around its z-axis
            otherObject.transform.Rotate(0f, 0f, deltaAngle / 12f);

            // Update the previous mouse position
            previousMousePosition = currentMousePosition;
        }
    }
}
