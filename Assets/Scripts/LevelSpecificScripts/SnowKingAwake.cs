using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReDesign{
public class SnowKingAwake : MonoBehaviour
{
    public bool AllPillarsDestroyed = false;
    private int pillars = 3;
    [SerializeField] GameObject SnowBoss;
    [SerializeField] GameObject Layer;
    //[SerializeField] private List<GameObject> Icicles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void pillardestroyed(){
        pillars += 1;
        if (pillars == 4){
            AllPillarsDestroyed = true;
            GameObject g = Instantiate(SnowBoss, transform.position, Quaternion.identity);
            g.transform.parent = Layer.transform;
            //SnowKingSequence
            pillars = 0;
        }
    }
}
}
