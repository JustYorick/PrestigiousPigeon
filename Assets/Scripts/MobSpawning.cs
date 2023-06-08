using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign {

public class MobSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> Mobs;
    [SerializeField] GameObject Layer;
    private bool spawning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // turncount counts number of turns of all entities combined!! 
        if(TurnController.TurnCount%4 == 0 && spawning == false){
            Spawn();
        }
        if(TurnController.TurnCount%4 != 0){
            spawning = false;
        }

    } 

    void Spawn(){
            spawning = true;
            if(!this.gameObject.scene.isLoaded) return;
            var r = Random.Range(0, Mobs.Count);
            GameObject m = Instantiate(Mobs[r], transform.position, Quaternion.identity);
            if (Layer.activeInHierarchy) {
                m.transform.parent = Layer.transform;
            }
            // WorldController.Instance.addObstacle(h);
    }
}
}
