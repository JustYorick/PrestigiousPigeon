using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    float moveSpeed = 0.25f;
    [SerializeField]
    float snapDistance = 0.25f;

    [SerializeField]
    Vector3 targetPosition = new Vector3(0, 1, 0);
    
    bool moving;

    void MovePlayer(){
        float distance = Vector3.Distance(transform.position, targetPosition);
        // Move the player, if the player isn't on the target position.
        if (Mathf.Abs(distance) > snapDistance && moving) {
            Vector3 distanceVector = transform.position - targetPosition;
            float totalDistance = Mathf.Abs(distanceVector.x) + Mathf.Abs(distanceVector.y) + Mathf.Abs(distanceVector.z);
            Vector3 movement = new Vector3(distanceVector.x / totalDistance, distanceVector.y / totalDistance, distanceVector.z / totalDistance);
            transform.position -= movement * moveSpeed * Time.deltaTime;
        } else {
            // If the player is on the target position
            // Print the target position
            Debug.Log(targetPosition);

            // Set the player position to the target position
            transform.position = targetPosition;

            // Stop moving
            moving = false;
        }
    }

    void SetTargetPosition(){
        // Move in the direction requested by the user, if the user presses a movement key
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
            targetPosition += Vector3.back;
            moving = true;
        } else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
            targetPosition += Vector3.forward;
            moving = true;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
            targetPosition += Vector3.left;
            moving = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
            targetPosition += Vector3.right;
            moving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player forward quickly, if the player pressed the left mouse button.
        // The player will move back to its original position after
        if(Input.GetButtonDown("Fire1")) {
            transform.position += Vector3.forward;
            moving = true;
        }

        // Set the target position on input
        SetTargetPosition();

        // Move the player
        MovePlayer();
    }
}
