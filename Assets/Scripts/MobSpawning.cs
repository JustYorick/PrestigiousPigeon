using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign {

public class MobSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> Mobs;
    [SerializeField] GameObject Layer;
    [SerializeField] int frequency = 2;
    private bool spawning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(TurnController.TurnCount%frequency == 0 && spawning == false){
            if(frequency == 1){
                Debug.Log("Spawning " + TurnController.TurnCount + " " + TurnController.TurnCount%frequency);
            }
            
            Spawn();
        }
        if(TurnController.TurnCount%frequency != 0){
            if(frequency == 1){
                Debug.Log("not Spawning " + TurnController.TurnCount + " " + TurnController.TurnCount%frequency);
            }
            spawning = false;
        }

    } 

    void Spawn(){
            spawning = true;
            if(!this.gameObject.scene.isLoaded) return;
            var r = Random.Range(0, Mobs.Count);
            GameObject m = Instantiate(Mobs[r], transform.position + new Vector3(1, .6f, 2), Quaternion.identity);
            if (m.gameObject.name.Contains("Slime")){
                m.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            
            if (Layer.activeInHierarchy) {
                m.transform.parent = Layer.transform;
            }
            WorldController.Instance.addObstacle(m);

            var a = Random.Range(0, Mobs.Count);
            GameObject o = Instantiate(Mobs[a], transform.position + new Vector3(-1, .6f, 2), Quaternion.identity);
            if (o.gameObject.name.Contains("Slime")){
                o.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
            
            if (Layer.activeInHierarchy) {
                o.transform.parent = Layer.transform;
            }
            WorldController.Instance.addObstacle(o);
    }
}
}
