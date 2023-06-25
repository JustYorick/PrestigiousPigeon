using System.Collections;
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
        [SerializeField] private GameObject playerObject;
        [SerializeField] private Vector2 ExtraAmountToMove;
        [SerializeField] private Animator _animatorForLvl2;
        private Vector3 prevPos;
        private Vector3 inputMoveDir { get; set; } = Vector3.zero;
        private float rotation { get; set; } = 0.0f;
        private Vector2 MinPos { get; set; } = new Vector2(0f,0f);
        private Vector2 MaxPos { get; set; } = new Vector2(0f,0f);
        private bool lockedToPlayer = false;
        private bool animationStopped = false;
        

        private void Start()
        {
            List<DefaultTile> baseLayer = WorldController.Instance.BaseLayer;
            GameObject firstGameObject = baseLayer.First().GameObject;
            GameObject lastGameObject = baseLayer.Last().GameObject;
            
            if(!playerObject)
                playerObject = GameObject.Find("Player");

            Vector3 minimalPosition = firstGameObject.transform.position;
            Vector3 maximalPosition = lastGameObject.transform.position;
            MinPos = new Vector2(minimalPosition.x, minimalPosition.z);
            MaxPos = new Vector2(maximalPosition.x, maximalPosition.z);
        }

        private void Update()
        {
                if (Input.GetKeyDown(KeyCode.Space))
                    lockedToPlayer = !lockedToPlayer;

                if (lockedToPlayer)
                {
                    transform.position = playerObject.transform.position;
                    transform.eulerAngles += new Vector3(0, rotation, 0) * (rotationSpeed * Time.deltaTime);
                }
                else if (!lockedToPlayer)
                {
                    // Calculate the direction to move the camera in, use the direction the camera is facing
                    Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;

                    // Move the camera
                    transform.position += moveVector * (moveSpeed * Time.deltaTime);
                    transform.eulerAngles += new Vector3(0, rotation, 0) * (rotationSpeed * Time.deltaTime);
                    transform.position = new Vector3
                    (
                        Mathf.Clamp(transform.position.x, MinPos.x - ExtraAmountToMove.x,
                            MaxPos.x + ExtraAmountToMove.x),
                        transform.position.y,
                        Mathf.Clamp(transform.position.z, MinPos.y - ExtraAmountToMove.y,
                            MaxPos.y + ExtraAmountToMove.y)
                    );

                    if (Input.GetKeyDown(KeyCode.Space))
                        transform.position = new Vector3(playerObject.transform.position.x, -1f,
                            playerObject.transform.position.z);
                }

                if (animationStopped)
                {
                    transform.position = prevPos;
                    transform.rotation = new Quaternion(0, 1, 0, 0);
                    if (Vector3.Distance(transform.localPosition, prevPos) < 0.2f) {
                        animationStopped = false;
                    }
                }
        }

        void OnMove(InputValue value){
            // Get the movement input
            Vector2 movement = value.Get<Vector2>();
            inputMoveDir = new Vector3(movement.x, 0f, movement.y);
        }

        // Get the rotation input
        void OnRotate(InputValue value) => rotation = value.Get<float>();
        
        public IEnumerator MoveSmoothlyTo(Vector3 destination, float timeToMove) {
            float totalMovementTime = timeToMove; // The amount of time you want the movement to take
            float currentMovementTime = 0f;// The amount of time that has passed
            Vector3 origin = transform.position; // First position of camera
            while (Vector3.Distance(transform.localPosition, destination) > 0.2f) {
                currentMovementTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(origin, destination, currentMovementTime / totalMovementTime);
                Debug.Log("infinite looooop");
                yield return null;
            }
        }

        public void MoveTo(Vector3 position)
        {
            Debug.Log("Moving camera");
            transform.position = position;
        }

        public void TurnOnAnimator(Vector3 pos)
        {
            _instance.prevPos = pos;
            Debug.Log(pos);
            Debug.Log(prevPos);

            _animatorForLvl2.enabled = true;
            _animatorForLvl2.Play("CameraAnimationLvl2");
        }

        public void SetPositionBack()
        {
            _animatorForLvl2.enabled = false;
            _animatorForLvl2.StopPlayback();
            animationStopped = true;
            Debug.Log(prevPos);
            _instance.MoveTo(_instance.prevPos);
        }
    }
}