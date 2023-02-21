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

    Vector3 targetPosition;
    
    bool moving;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            transform.position += Vector3.forward;
            moving = true;
        }

        if (Vector3.Distance(transform.position, targetPosition) > snapDistance && moving) {
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        } else {
            Debug.Log(targetPosition);
            transform.position = targetPosition;
            moving = false;
        }

        if (Input.GetAxisRaw("Vertical") == 1f) {
            targetPosition = transform.position + Vector3.forward;
            moving = true;
        } else if (Input.GetAxisRaw("Vertical") == -1f) {
            targetPosition = transform.position + Vector3.back;
            moving = true;
        }
    }
}
