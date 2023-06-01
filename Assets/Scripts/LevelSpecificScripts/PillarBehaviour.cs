using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign{
public class PillarBehaviour : MonoBehaviour
{
    [SerializeField] GameObject TriggerOnDestroy;
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
        TriggerOnDestroy.GetComponent<SnowKingAwake>().pillardestroyed();
        // Spawns (more) Skeletons/Liches?
    }
}
}
