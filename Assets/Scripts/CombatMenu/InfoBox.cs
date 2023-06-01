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
            DefaultTile tile = mouseController.MouseToTile();

            // Try to retrieve the entity on that tile
            Entity entity = GetEntity(tile);

            // If there was an entity
            if(entity != null){
                // Draw the range of that entity
                RangeTileTool.Instance.drawMoveRange(tile, entity.MoveRange);
                
                // Show entity info in the info box
                ShowEntityInfo(entity);
            }
        }
    }

    Entity GetEntity(DefaultTile tile){
        // Find the first tile with the same position as the tile the player clicked on
        GameObject entityTile = WorldController.ObstacleLayer.FirstOrDefault(t => t.XPos == tile.XPos && t.YPos == tile.YPos).GameObject;

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
}
