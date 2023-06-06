using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign{
public class SnowKingAwake : MonoBehaviour
{
    public bool AllPillarsDestroyed = false;
    private int pillars = 3;
    [SerializeField] GameObject SnowBoss;
    [SerializeField] GameObject Skeleton;
    [SerializeField] GameObject Layer;

    [SerializeField] ParticleSystem snow;

    // Start is called before the first frame update
    void Start()
    {
        snow.Pause(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void pillardestroyed(Vector3 pos){
        pillars += 1;

        if(!this.gameObject.scene.isLoaded) return;
        // spawn Skeleton
        if(WorldController.Instance.checkNode(pos)){           // check if tile occupied
            GameObject h = Instantiate(Skeleton, pos, Quaternion.Euler(-90, 0, 0));
            if (Layer.activeInHierarchy) {
                h.transform.parent = Layer.transform;
            }
            WorldController.Instance.addObstacle(h);
        }
        // spawn Snow King
        if (pillars == 4){
            AllPillarsDestroyed = true;
            GameObject g = Instantiate(SnowBoss, transform.position, Quaternion.Euler(-90, 0, 0));
            if (Layer.activeInHierarchy) {
                g.transform.parent = Layer.transform;
            }
            WorldController.Instance.addObstacle(g);

            pillars = 0;
            if(snow != null){snow.Play(true);}
            
            
        }
    }
}
}
