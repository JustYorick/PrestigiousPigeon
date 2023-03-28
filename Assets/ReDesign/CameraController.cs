using UnityEngine;

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
        
        
        
        
    }
}