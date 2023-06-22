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
    [SerializeField] ParticleSystem SpawnParticles;
    private bool spawning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(TurnController.TurnCount%frequency == frequency-1 && spawning == false){
            Spawn();
        }
        if(TurnController.TurnCount%frequency != frequency-1){
            spawning = false;
        }

    } 

    void Spawn(){
            spawning = true;
            if(!this.gameObject.scene.isLoaded) return;
            var r = Random.Range(0, Mobs.Count);
            float i = .3f;
            if (r == 0){
                i = .6f;
            }
            GameObject m = Instantiate(Mobs[r], transform.position + new Vector3(0, i, 2), Quaternion.identity);
            SpawnParticles.transform.position = m.transform.position;
            SpawnParticles.Play();
            
            if (Layer.activeInHierarchy) {
                m.transform.parent = Layer.transform;
            }
            WorldController.Instance.addObstacle(m);

            // var a = Random.Range(0, Mobs.Count);
            // GameObject o = Instantiate(Mobs[a], transform.position + new Vector3(-1, .6f, 2), Quaternion.identity);
            // if (o.gameObject.name.Contains("Slime")){
            //     o.transform.localScale = new Vector3(.5f, .5f, .5f);
            // }
            
            // if (Layer.activeInHierarchy) {
            //     o.transform.parent = Layer.transform;
            // }
            // WorldController.Instance.addObstacle(o);
    }
}
}
