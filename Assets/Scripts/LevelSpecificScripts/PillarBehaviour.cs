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

        if (TriggerOnDestroy != null){
            TriggerOnDestroy.GetComponent<SnowKingAwake>().pillardestroyed(transform.position + new Vector3(0, -1, 1));
        }
        
    }
}
}
