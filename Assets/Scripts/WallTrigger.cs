using ReDesign.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign {
public class WallTrigger : MonoBehaviour
{
    private static List<Entity> _entities = new List<Entity>();
    private int turn = 0;
    public static bool CityEntered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (TurnController.TurnCount != turn){
            turn = TurnController.TurnCount;
            FillEntityList();
        }

    }

    public void FillEntityList()
        {
            _entities = WorldController.getEntities();
            foreach (Entity e in _entities){
                if(!e.name.Contains("Player") && e.transform.position.z > 10){
                    TurnController.gameOver = true;
                    TurnController.Instance.gameOverEvent.Invoke();
                }
                
            }
            
        }

}
}
