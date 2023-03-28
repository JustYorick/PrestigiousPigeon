using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReDesign
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private static MouseController _instance;
        public static MouseController Instance { get { return _instance; } }
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
            if (Input.GetMouseButtonDown(0))
            {
               // Debug.Log("on mouse down");
                Vector3 pos = GetMouseWorldPos();
               // Debug.Log("moveplayer:" + pos.x);

                //Debug.Log(" "+player.transform.position.y);



                //List<DefaultTile> pathNodesMap = this.GetComponent<WorldController>().BaseLayer;
                GridLayout gr = WorldController.Instance.gridLayout;
                //Debug.Log("" + gr.name);
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                Debug.Log("" + pathNodesMap.Count);
                player.GetComponent<PlayerMovement>().MovePlayer(pos, gr, pathNodesMap);
                //Debug.Log("" + player.GetComponent<PlayerMovement>().tag);
                
                
            }

            // Temp
            if (Input.GetKeyDown("i"))
            {
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                Debug.Log("i pressed");
                List<DefaultTile> affectedNodes = new List<DefaultTile>() { player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap) };
                WorldController.Instance.GetComponent<EnvironmentEffect>().IceEnvironmentEffects(affectedNodes);
            }
            // Temp 2
            if (Input.GetKeyDown("o"))
            {
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                Debug.Log("o pressed");
                List<DefaultTile> affectedNodes = new List<DefaultTile>() { player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap) };
                //affectedNodes = new List<PathNode>() { pathNodesMap.Where(p => p.x == 1 && p.y == 4).FirstOrDefault(),
                //    pathNodesMap.Where(p => p.x == 2 && p.y == 8).FirstOrDefault(),
                //    pathNodesMap.Where(p => p.x == 8 && p.y == 8).FirstOrDefault(),
                //};
                WorldController.Instance.GetComponent<EnvironmentEffect>().FireEnvironmentEffects(affectedNodes);
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
    }
}