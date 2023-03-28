using UnityEngine;
using UnityEngine.Serialization;

namespace ReDesign
{
    public class StateController : MonoBehaviour
    {
        private static StateController _instance;
        public static StateController Instance { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }


        public GameState currentState { get; private set; } = GameState.Idle;

        public void ChangeState(GameState state)
        {
            currentState = state;
        }
    }

    public enum GameState
    {
        Idle,
        ballin,
        offDaZoinks,
    }
}