using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReDesign
{
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float moveSpeed = 10f;
        private Vector3 inputMoveDir = Vector3.zero;
        private float rotation = 0.0f;
        private Vector2 minPos = new Vector2(0f,0f);
        private Vector2 maxPos = new Vector2(0f,0f);
        private void Start()
        {
            // Gets the minimal position the camera can travel in the level
            minPos = new Vector2(WorldController.Instance.BaseLayer.First().GameObject.transform.position.x,
                WorldController.Instance.BaseLayer.First().GameObject.transform.position.z);
            // Gets the Maximal position the camera can travel in the level
            maxPos = new Vector2(WorldController.Instance.BaseLayer.Last().GameObject.transform.position.x,
                WorldController.Instance.BaseLayer.Last().GameObject.transform.position.z);
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log("minPos: " +minPos + "\nmaxpos: " + maxPos);
            // Checks for position
            // if (gameObject.transform.position.x >= minPos.x &&
            //     gameObject.transform.position.y >= minPos.y &&
            //     gameObject.transform.position.x <= maxPos.x &&
            //     gameObject.transform.position.y <= maxPos.y)
            // {
            // }
                // Calculate the direction to move the camera in, use the direction the camera is facing
                Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;

                // Move the camera
                transform.position += moveVector * (moveSpeed * Time.deltaTime);

                // Rotate the camera
                transform.eulerAngles += new Vector3(0, rotation, 0) * (rotationSpeed * Time.deltaTime);
        }

        void OnMove(InputValue value){
            // Get the movement input
            Vector2 movement = value.Get<Vector2>();
            if (gameObject.transform.position.x >= minPos.x &&
                gameObject.transform.position.y >= minPos.y &&
                gameObject.transform.position.x <= maxPos.x &&
                gameObject.transform.position.y <= maxPos.y)
            {
                // Use the x and y for movement on the x and z axis, respectively
                inputMoveDir = new Vector3(movement.x, 0f, movement.y);
            }else{
                // check if camera empty is in topright
                if (gameObject.transform.position.x >= maxPos.x
                    && gameObject.transform.position.y >= maxPos.y)
                {
                    Debug.Log("in topRight");
                    if (movement is { x: < 0f, y: < 0f })
                        inputMoveDir = new Vector3(movement.x, 0f, movement.y);
                }
                // check if camera empty is in bottomLeft
                else if (gameObject.transform.position.x <= minPos.x
                         && gameObject.transform.position.y <= minPos.y)
                {
                    Debug.Log("in bottomLeft");

                    if (movement is { x: > 0f, y: > 0f })
                        inputMoveDir = new Vector3(movement.x, 0f, movement.y);
                }
                // check if camera empty is in topLeft
                else if (gameObject.transform.position.x <= minPos.x
                         && gameObject.transform.position.y >= maxPos.y)
                {
                    Debug.Log("in topLeft");

                    if (movement is { x: > 0f, y: < 0f })
                        inputMoveDir = new Vector3(movement.x, 0f, movement.y);
                }
                // check if camera empty is in bottomRight
                else if (gameObject.transform.position.x >= maxPos.x
                         && gameObject.transform.position.y <= minPos.y)
                {
                    if (movement is { x: < 0f, y: > 0f })
                        inputMoveDir = new Vector3(movement.x, 0f, movement.y);
                }
            }




        }

        // Get the rotation input
        void OnRotate(InputValue value) => rotation = value.Get<float>();
        
        
    }
}