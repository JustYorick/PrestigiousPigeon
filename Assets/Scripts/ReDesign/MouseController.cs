using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace ReDesign
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;
        [SerializeField] private ManaSystem manaSystem;
        [SerializeField] private Tilemap SelectorMap;
        private static MouseController _instance;
        private bool drawSelectedTile = true;
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
            DrawCurrentSelectedTile();
            
            DrawCurrentSpellRange();
            
            List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
            {
                Vector3 pos = GetMouseWorldPos();
                GridLayout gr = WorldController.Instance.gridLayout;
                player.ShowPath(pos, gr, pathNodesMap);
            }
            if(!Input.GetMouseButtonDown(0)){
                return;
            }
            if (spellSelection == null)
            {
                Vector3 pos = GetMouseWorldPos();
                GridLayout gr = WorldController.Instance.gridLayout;
                player.MovePlayer(pos, gr, pathNodesMap);
            }else{
                int playerPosX = player.FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).XPos;
                int playerPosY = player.FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).YPos;
                if (spellSelection.GetTargetLocations(playerPosX, playerPosY).Contains(player.FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap)) && manaSystem.GetMana()>= spellSelection.ManaCost)
                {
                    int x = player.FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).XPos;
                    int y = player.FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).YPos;
                    spellSelection.Effect(x, y);
                    manaSystem.UseMana(spellSelection.ManaCost);
                }
                spellSelection = null;
            }
        }

        private Vector3 GetMouseWorldPos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                drawSelectedTile = true;
                return raycastHit.point;
            }
            else
            {
                drawSelectedTile = false;
                return Vector3.zero;
            }
        }

        public DefaultTile MouseToTile()
        {
            DefaultTile hoveredNode = WorldController.Instance.BaseLayer
                .OrderBy(item => Math.Abs(GetMouseWorldPos().x - item.GameObject.transform.position.x))
                .ThenBy(item => Math.Abs(GetMouseWorldPos().z - item.GameObject.transform.position.z)).ToList()
                .FirstOrDefault();

            return hoveredNode;
        }
        
        public void SelectFireSpell() => spellSelection = new BasicFireSpell();
        public void SelectIceSpell() => spellSelection = new BasicIceSpell();
        
        private void DrawCurrentSelectedTile()
        {
            Color color = new Color(255, 255, 255, 0.05f);
            DefaultTile hoveredNode = MouseToTile();
            RangeTileTool.Instance.clearTileMap(SelectorMap);
            if (hoveredNode != null && drawSelectedTile)
            {
                RangeTileTool.Instance.SpawnTile(hoveredNode.XPos, hoveredNode.YPos, color, SelectorMap, false);
            }
        }

        private void DrawCurrentSpellRange()
        {
            if (spellSelection != null)
            {
                DefaultTile playerPos = player.FindNearestXYPathNode(player.gameObject.transform.position, WorldController.Instance.BaseLayer);
                List<DefaultTile> targets = spellSelection.GetTargetLocations(playerPos.XPos, playerPos.YPos);

                foreach (var t in targets)
                {
                    RangeTileTool.Instance.SpawnTile(t.XPos,t.YPos, new Color(255,0,0,0.5f), SelectorMap, false);
                }
            }
        }
    }
}