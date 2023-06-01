using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign{
public class SnowKingAwake : MonoBehaviour
{
    public bool AllPillarsDestroyed = false;
    private int pillars = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pillars == 4){
            AllPillarsDestroyed = true;
            //SnowKingSequence
        }
    }

    public void pillardestroyed(){
        pillars += 1;
    }
}
}
