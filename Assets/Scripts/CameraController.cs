using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float moveSpeed = 10f;
    private Vector3 inputMoveDir = Vector3.zero;
    private float rotation = 0.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, rotation, 0) * rotationSpeed * Time.deltaTime;
    }

    void OnMove(InputValue value){
        Vector2 movement = value.Get<Vector2>();
        inputMoveDir = new Vector3(movement.x, 0f, movement.y);
    }

    void OnRotate(InputValue value) => rotation = value.Get<float>();
}
