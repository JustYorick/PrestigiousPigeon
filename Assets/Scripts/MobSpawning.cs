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

        if(TurnController.TurnCount%3 == 1 && spawning == false){
            Spawn();
        }
        if(TurnController.TurnCount%3 != 1){
            spawning = false;
        }

    } 

    void Spawn(){
            spawning = true;
            if(!this.gameObject.scene.isLoaded) return;
            var r = Random.Range(0, Mobs.Count);
            GameObject m = Instantiate(Mobs[r], transform.position + new Vector3(r, .6f, 2), Quaternion.identity);
            if (m.gameObject.name.Contains("Slime")){
                m.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            
            if (Layer.activeInHierarchy) {
                m.transform.parent = Layer.transform;
            }
            WorldController.Instance.addObstacle(m);
    }
}
}
