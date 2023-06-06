using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReDesign
{
    public class CameraController : MonoBehaviour
    {
        private static CameraController _instance;
        public static CameraController Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            
            _instance = this;
        }
        
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float moveSpeed = 5f;
        private Vector3 inputMoveDir { get; set; } = Vector3.zero;
        private float rotation { get; set; } = 0.0f;
        private Vector2 MinPos { get; set; } = new Vector2(0f,0f);
        private Vector2 MaxPos { get; set; } = new Vector2(0f,0f);
        [SerializeField] private Vector2 ExtraAmountToMove;
        
        private void Start()
        {
            List<DefaultTile> baseLayer = WorldController.Instance.BaseLayer;
            GameObject firstGameObject = baseLayer.First().GameObject;
            GameObject lastGameObject = baseLayer.Last().GameObject;

            Vector3 minimalPosition = firstGameObject.transform.position;
            Vector3 maximalPosition = lastGameObject.transform.position;
            MinPos = new Vector2(minimalPosition.x, minimalPosition.z);
            MaxPos = new Vector2(maximalPosition.x, maximalPosition.z);
        }

        private void Update()
        {
            // Calculate the direction to move the camera in, use the direction the camera is facing
            Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;

            // Move the camera
            transform.position += moveVector * (moveSpeed * Time.deltaTime);
            transform.eulerAngles += new Vector3(0, rotation, 0) * (rotationSpeed * Time.deltaTime);            
            transform.position = new Vector3 
            (
                Mathf.Clamp (transform.position.x, MinPos.x - ExtraAmountToMove.x, MaxPos.x + ExtraAmountToMove.x), 
                0.0f, 
                Mathf.Clamp (transform.position.z, MinPos.y - ExtraAmountToMove.y, MaxPos.y + ExtraAmountToMove.y)
            );
        }

        void OnMove(InputValue value){
            // Get the movement input
            Vector2 movement = value.Get<Vector2>();
            inputMoveDir = new Vector3(movement.x, 0f, movement.y);
        }

        // Get the rotation input
        void OnRotate(InputValue value) => rotation = value.Get<float>();
    }
}