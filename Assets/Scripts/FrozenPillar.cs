using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReDesign;
using UnityEngine;

public class FrozenPillar : MonoBehaviour
{
    [SerializeField] private int MeltInXturns = 2;
    private int startingTurnCount;

    // Start is called before the first frame update
    void Start()
    {
        startingTurnCount = TurnController.TurnCount;
    }

    // Update is called once per frame
    void Update()
    {
        meltTimer();
    }

    private void meltTimer()
    {
        if (TurnController.TurnCount == startingTurnCount + MeltInXturns)
        {
            var position = gameObject.transform.position;
            
            DefaultTile targetTile = WorldController.Instance.BaseLayer.Where(t => t.GameObject.transform.position.x == position.x && t.GameObject.transform.position.z == position.z).FirstOrDefault();
            var locationList = new List<DefaultTile>();
            locationList.Add(targetTile);
            GameObject.Find("WorldController").GetComponent<EnvironmentEffect>().ChangeFrozenPillarTilesToPuddle(locationList);
        } 
    }
}
