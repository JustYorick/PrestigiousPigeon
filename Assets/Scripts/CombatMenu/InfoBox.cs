using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ReDesign;
using ReDesign.Entities;

public class InfoBox : MonoBehaviour{
    [SerializeField] private TMPro.TMP_Text entityTitleText;
    [SerializeField] private TMPro.TMP_Text entityHpText;
    [SerializeField] private TMPro.TMP_Text entityMoveRangeText;
    private ReDesign.MouseController mouseController;
    private ReDesign.Entities.Entity player;

    void Start(){
        // Retrieve the mouse controller and player
        mouseController = FindObjectOfType<MouseController>();
        player = FindObjectOfType<Player>();

        // Show entity info in the info box
        ShowEntityInfo(player);
    }

    void Update(){
        // If the user presses the right mouse button
        if (Input.GetMouseButtonDown(1)){
            // Calculate the tile the player clicked on
            DefaultTile tile = mouseController.MouseToTile(MouseController.GetMouseWorldPos());

            // Try to retrieve the entity on that tile
            Entity entity = GetEntity(tile);

            // If there was an entity
            if(entity != null){
                // Draw the range of that entity
                RangeTileTool.Instance.drawMoveRange(tile, entity.MoveRange);
                
                // Show entity info in the info box
                ShowEntityInfo(entity);
            } 
            
            else {
                GameObject go = GetObject(tile);
                if (go != null){
                    ShowObjectInfo(go);
                } else {
                    ShowTileInfo(tile);
                }
            }


        }
    }

    Entity GetEntity(DefaultTile tile){
        // Find the first tile with the same position as the tile the player clicked on
        GameObject entityTile = WorldController.ObstacleLayer.FirstOrDefault(t => t.XPos == tile.XPos && t.YPos == tile.YPos)?.GameObject;

        // If no tile was found or the there is no entity on that tile, return null
        if(entityTile == null || !entityTile.CompareTag("Entity")){
            return null;
        }

        // Return the entity on the tile
        return entityTile.GetComponent<Entity>();
    }

    void ShowEntityInfo(Entity entity){
        // Set the entity info
        entityTitleText.text = entity.displayName;
        entityHpText.text = $"HP: {entity._entityHealth.Health}";
        entityMoveRangeText.text = $"Move Range: {entity.MoveRange}";
    }



    GameObject GetObject(DefaultTile tile){
        // Find the first tile with the same position as the tile the player clicked on
        GameObject g = WorldController.ObstacleLayer.FirstOrDefault(t => t.XPos == tile.XPos && t.YPos == tile.YPos)?.GameObject;

        // If no tile was found or the there is no object on that tile, return null
        if(g == null || g.CompareTag("Entity")){
            return null;
        }

        // Return the objecton the tile
        return g;
    }

    void ShowObjectInfo(GameObject g){
        // Set the object info
        entityHpText.text = $"Spell: ";
        if(g.name.ToLower().Contains("tree")){
            entityTitleText.text = "Tree";
            entityMoveRangeText.text = $"Fire";
        }
        if (g.name.ToLower().Contains("pillar1")){
            entityTitleText.text = "Inactive Obelisk";
            entityMoveRangeText.text = $"Ice";
        }
        if(g.name.ToLower().Contains("obelisk")){
            entityTitleText.text = "Obelisk";
            entityMoveRangeText.text = $"Ice";
            if(g.transform.childCount > 0){
                Transform p = g.transform.GetChild(0);
                p.gameObject.SetActive(!p.gameObject.activeInHierarchy);
            }
            
        }
        if(g.name.ToLower().Contains("grave")){
            entityTitleText.text = "Grave";
            entityMoveRangeText.text = $"Ice";
        }
        if(g.name.ToLower().Contains("rock")){
            entityTitleText.text = "Rock";
            entityMoveRangeText.text = $"-";
        }
        if(g.name.ToLower().Contains("fence")){
            entityTitleText.text = "Fence";
            entityMoveRangeText.text = $"-";
        }
        if(g.name.ToLower().Contains("icicle")){
            entityTitleText.text = "Icicle";
            entityMoveRangeText.text = $"Fire";
        }
    }

    void ShowTileInfo(DefaultTile tile){
        GameObject Tile = WorldController.Instance.BaseLayer.FirstOrDefault(t => t.XPos == tile.XPos && t.YPos == tile.YPos)?.GameObject;
        if (Tile != null){
            entityTitleText.text = Tile.name;
            entityHpText.text = $"Spell: ";
            if (Tile.name == "Water"){
                entityMoveRangeText.text = $"Ice";
            }
            if (Tile.name == "Grass" || Tile.name == "GraveyardGravel" || Tile.name == "Stone"){
                entityMoveRangeText.text = $"-";
            }
            if (Tile.name == "Ice"){
                entityMoveRangeText.text = $"Fire";
            }
            if (Tile.name.ToLower().Contains("bridge")){
                entityTitleText.text = "Bridge";
                entityMoveRangeText.text = $"Fire";
            }
            
        }
    }

}
