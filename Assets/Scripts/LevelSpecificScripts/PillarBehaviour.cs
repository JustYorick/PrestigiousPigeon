using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign{
public class PillarBehaviour : MonoBehaviour
{
    [SerializeField] GameObject TriggerOnDestroy;
    [SerializeField] GameObject Skeleton;
    [SerializeField] GameObject Layer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        Vector3 pos = transform.position + new Vector3(0, 0, 1);
        GameObject g = Instantiate(Skeleton, pos, Quaternion.identity);
        g.transform.parent = Layer.transform;
        WorldController.Instance.addObstacle(g);
        if (TriggerOnDestroy.GetComponent<SnowKingAwake>() != null){
            TriggerOnDestroy.GetComponent<SnowKingAwake>().pillardestroyed();
            // Spawns (more) Skeletons/Liches?
        }
        
    }
}
}
