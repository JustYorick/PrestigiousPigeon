using System.Collections;
using UnityEngine;

public class TempEnemyMovement : MonoBehaviour
{
    public TurnSystemScript turnSystem;

    public TurnClass turnClass;

    public bool isTurn = false;


    public KeyCode moveKey;
    // Start is called before the first frame update
    void Start()
    {
        turnSystem = GameObject.Find("Turn-Manager").GetComponent<TurnSystemScript>();

        foreach (TurnClass tc in turnSystem.playersGroup)
        {
            if (tc.playerGameObject.name == gameObject.name)
            {
                turnClass = tc;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        isTurn = turnClass.isTurn;
        

        if (isTurn)
        {
            StartCoroutine(nameof(WaitAndMove));
        }
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(1f);
        
        transform.position += Vector3.forward;
        isTurn = false;
        turnClass.isTurn = isTurn;
        
        // Makes camera turn to activePlayer
        turnSystem.isSwitched = true;
        turnClass.wasTurnPrev = true;
        
        StopCoroutine(nameof(WaitAndMove));
    }
}