using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign {

public class MobSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Mob;
    [SerializeField] int difficulty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnController.TurnCount%2 == 0){
            GameObject m = Instantiate(Mob, transform.position, Quaternion.identity);
        }
    } 
}
}
