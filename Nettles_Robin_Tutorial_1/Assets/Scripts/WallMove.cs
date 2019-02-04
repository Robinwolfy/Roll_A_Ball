using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;
    public bool forward;
    //bool reverse;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        forward = false;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Follows the target position like with a spring
    void Update()
    {
        // Distance moved = time * speed.
        //float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        //float fracJourney = distCovered / journeyLength;

        /*if (forward)
        {// Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, speed);
        }
        if (!forward)
        {
            transform.position = Vector3.Lerp(endMarker.position, startMarker.position, speed);
        }*/
        // Set the x position to loop between 0 and 3
        //transform.position = new Vector3(Mathf.PingPong(Time.time, startMarker.position.x ), transform.position.y, transform.position.z);
        if (forward)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, endMarker.position, step);
        }
        if (!forward)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, startMarker.position, step);
        }
        // Check if the position of the cube and sphere are approximately equal.
        /*if (Vector3.Distance(transform.position, endMarker.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            endMarker.position *= -1.0f;
        }*/

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("end"))
        {
            forward = false;
        }
        if (other.gameObject.CompareTag ("start"))
        {
            forward = true;
        }
    }
}
