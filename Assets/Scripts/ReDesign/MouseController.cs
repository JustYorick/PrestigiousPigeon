using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReDesign
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private ManaSystem manaSystem;
        private static MouseController _instance;
        public static MouseController Instance { get { return _instance; } }
        private AttacksAndSpells spellSelection = null;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        private void Update()
        {
            List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
            {
                Vector3 pos = GetMouseWorldPos();
                GridLayout gr = WorldController.Instance.gridLayout;
                player.GetComponent<PlayerMovement>().ShowPath(pos, gr, pathNodesMap);
            }
            if(!Input.GetMouseButtonDown(0)){
                return;
            }
            if (spellSelection == null)
            {
                Vector3 pos = GetMouseWorldPos();
                GridLayout gr = WorldController.Instance.gridLayout;
                player.GetComponent<PlayerMovement>().MovePlayer(pos, gr, pathNodesMap);
            }else{
                int playerPosX = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).XPos;
                int playerPosY = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).YPos;
                if (spellSelection.GetTargetLocations(playerPosX, playerPosY).Contains(player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap)) && manaSystem.GetMana()>=2)
                {

                    int x = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).XPos;
                    int y = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).YPos;
                    spellSelection.Effect(x, y);
                    manaSystem.UseMana(2);
                    spellSelection = null;
                }
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public void SelectFireSpell() => spellSelection = new BasicFireSpell();
        public void SelectIceSpell() => spellSelection = new BasicIceSpell();
    }
}