﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace ReDesign
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Tilemap SelectorMap;
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
            DrawCurrentSelectedTile();
            {
                Vector3 pos = GetMouseWorldPos();
                GridLayout gr = WorldController.Instance.gridLayout;
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                player.GetComponent<PlayerMovement>().ShowPath(pos, gr, pathNodesMap);
            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = GetMouseWorldPos();
                GridLayout gr = WorldController.Instance.gridLayout;
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                player.GetComponent<PlayerMovement>().MovePlayer(pos, gr, pathNodesMap);
            }

            // Temp
            if (Input.GetKeyDown("i"))
            {
                Debug.Log("i pressed");
                BasicIceSpell ice = new BasicIceSpell();
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                int playerPosX = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).XPos;
                int playerPosY = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).YPos;
                if (ice.GetTargetLocations(playerPosX, playerPosY).Contains(player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap)))
                {
                    int x = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).XPos;
                    int y = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).YPos;
                    ice.Effect(x, y);
                }
            }

            // Temp 2
            if (Input.GetKeyDown("o"))
            {
                Debug.Log("o pressed");
                /*List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                List<DefaultTile> affectedNodes = new List<DefaultTile>() { player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap) };
                WorldController.Instance.GetComponent<EnvironmentEffect>().FireEnvironmentEffects(affectedNodes);
                */
                BasicFireSpell fire = new BasicFireSpell();
                List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;
                int playerPosX = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).XPos;
                int playerPosY = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).YPos;
                if (fire.GetTargetLocations(playerPosX, playerPosY).Contains(player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap)))
                {
                    int x = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).XPos;
                    int y = player.GetComponent<PlayerMovement>().FindNearestXYPathNode(GetMouseWorldPos(), pathNodesMap).YPos;
                    fire.Effect(x, y);
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

        public DefaultTile MouseToTile()
        {
            DefaultTile hoveredNode = WorldController.Instance.BaseLayer
                .OrderBy(item => Math.Abs(GetMouseWorldPos().x - item.GameObject.transform.position.x))
                .ThenBy(item => Math.Abs(GetMouseWorldPos().z - item.GameObject.transform.position.z)).ToList()
                .FirstOrDefault();

            return hoveredNode;
        }

        private void DrawCurrentSelectedTile()
        {
            Color color = new Color(255, 255, 255, 0.05f);
            DefaultTile hoveredNode = MouseToTile();
            RangeTileTool.Instance.clearTileMap(SelectorMap);
            if (hoveredNode != null)
            {
                RangeTileTool.Instance.SpawnTile(hoveredNode.XPos, hoveredNode.YPos, color, SelectorMap, false);
            }
        }
    }
}