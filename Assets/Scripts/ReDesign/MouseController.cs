﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReDesign.Entities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ReDesign
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private PlayerMovement player;
        [SerializeField] private StatusBar manaSystem;
        [SerializeField] private Tilemap SelectorMap;
        private static MouseController _instance;
        private static bool drawSelectedTile = true;
        private Vector3 targetLocation;
        public static MouseController Instance { get { return _instance; } }
        public static AttacksAndSpells spellSelection = null;
        public ParticleSystem fireParticles;
        public ParticleSystem iceParticles;
        private DefaultTile prevSelectedTile;
        [SerializeField] private SpellMenu spellMenu;
        Vector3 previousMousePosition = Vector3.zero;
        private BasicFireSpell fireSpell;
        private BasicIceSpell iceSpell;

        private void Awake()
        {
            fireSpell = new BasicFireSpell(fireParticles);
            iceSpell = new BasicIceSpell(iceParticles);

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        private void Update()
        {
            Vector3 mousePosition = GetMouseWorldPos();
            DrawCurrentSelectedTile(MouseToTile(mousePosition));
            
            DrawCurrentSpellRange();
            
            List<DefaultTile> pathNodesMap = WorldController.Instance.BaseLayer;

            if(mousePosition != previousMousePosition){
                GridLayout gr = WorldController.Instance.gridLayout;
                player.ShowPath(mousePosition, gr, pathNodesMap);
            }
            if(!Input.GetMouseButtonDown(0)){
                return;
            }
            if (spellSelection == null)
            {
                GridLayout gr = WorldController.Instance.gridLayout;
                player.MovePlayer(mousePosition, gr, pathNodesMap);
            }else{
                int playerPosX = player.FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).XPos;
                int playerPosY = player.FindNearestXYPathNode(player.gameObject.transform.position, pathNodesMap).YPos;
                if (spellSelection.GetTargetLocations(playerPosX, playerPosY).Contains(player.FindNearestXYPathNode(mousePosition, pathNodesMap)) && manaSystem.Value >= spellSelection.ManaCost)
                {
                    DefaultTile nearestPathNode = player.FindNearestXYPathNode(mousePosition, pathNodesMap);
                    int x = nearestPathNode.XPos;
                    int y = nearestPathNode.YPos;
                    StartCoroutine(Player.RotateToAttack());
                    CheckSpellCasted(spellSelection);
                    spellSelection.Effect(x, y);
                    manaSystem.Value -= spellSelection.ManaCost;
                }
                spellSelection = null;
                spellMenu.Close();
                RangeTileTool.Instance.clearTileMap(SelectorMap);
                CheckSpellCasted(spellSelection);
                StopCoroutine(Player.RotateToAttack());
            }
        }

        public static Vector3 GetMouseWorldPos()
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

        public DefaultTile MouseToTile(Vector3 mousePosition)
        {
            DefaultTile hoveredNode = WorldController.Instance.BaseLayer
                .OrderBy(item => Math.Abs(mousePosition.x - item.GameObject.transform.position.x))
                .ThenBy(item => Math.Abs(mousePosition.z - item.GameObject.transform.position.z)).ToList()
                .FirstOrDefault();

            return hoveredNode;
        }
        
        public void SelectFireSpell(){
            RangeTileTool.Instance.clearTileMap(RangeTileTool.Instance.rangeTileMap);
            if(fireSpell.ManaCost <= manaSystem.Value){
                spellSelection = fireSpell;
            }else{
                spellSelection = null;
            }
        }

        public void SelectIceSpell(){
            RangeTileTool.Instance.clearTileMap(RangeTileTool.Instance.rangeTileMap);
            if(iceSpell.ManaCost <= manaSystem.Value){
                spellSelection = iceSpell;
            }else{
                spellSelection = null;
            }
        }
        
        public void DeselectSpell() => spellSelection = null;
        
        private void DrawCurrentSelectedTile(DefaultTile hoveredNode)
        {
            if (hoveredNode != prevSelectedTile)
            {
                Color color = new Color(255, 255, 255, 0.05f);
                prevSelectedTile = hoveredNode;
                RangeTileTool.Instance.clearTileMap(SelectorMap);
                if (hoveredNode != null && drawSelectedTile)
                {
                    RangeTileTool.Instance.SpawnTile(hoveredNode.XPos, hoveredNode.YPos, color, SelectorMap, false);
                }
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
                    RangeTileTool.Instance.SpawnTile(t.XPos,t.YPos, new Color(0,0,255,0.2f), SelectorMap, false);
                }
            }
        }
        
        private static void CheckSpellCasted(AttacksAndSpells spellSelection)
        {
            if (spellSelection != null)
            {
                if (spellSelection.GetType() == typeof(BasicFireSpell)) PlayerAnimator._animator.SetBool("fireCasted", true);

                else if (spellSelection.GetType() == typeof(BasicIceSpell)) PlayerAnimator._animator.SetBool("iceCasted", true);
            }
        }
    }
}