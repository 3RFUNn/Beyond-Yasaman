using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassRotator : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circleCollider;
// The speed of rotation in degrees per second
    public float rotateSpeed;

    // The initial touch position
    private Vector2 touchStart;

    // The current touch position
    private Vector2 touchCurrent;

    // The angle between the arrow and the touch position
    private float touchAngle;

    // The flag to indicate if the touch is inside the circle collider
    private bool touchInside;

    // Update is called once per frame
    void Update()
    {
        // Check if there is any touch input
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            // Check the touch phase
            switch (touch.phase)
            {
                // When the touch begins, store the initial position and check if it is inside the circle collider
                case TouchPhase.Began:
                    touchStart = Camera.main.ScreenToWorldPoint(touch.position);
                    touchInside = circleCollider.OverlapPoint(touchStart);
                    break;
                // When the touch moves, update the current position and calculate the angle of rotation
                case TouchPhase.Moved:
                    if (touchInside)
                    {
                        touchCurrent = Camera.main.ScreenToWorldPoint(touch.position);
                        touchAngle = Mathf.Atan2(touchCurrent.y - transform.position.y, touchCurrent.x - transform.position.x) * Mathf.Rad2Deg;
                    }
                    break;
                // When the touch ends, reset the flag
                case TouchPhase.Ended:
                    touchInside = false;
                    break;
            }
        }

        // Rotate the game object to face the touch position smoothly
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, touchAngle - 90f), rotateSpeed * Time.deltaTime);
    }
}



