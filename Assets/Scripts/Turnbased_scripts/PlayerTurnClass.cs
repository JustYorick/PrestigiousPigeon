using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTurnClass : MonoBehaviour
{
    public TurnSystemScript turnSystem;

    public TurnClass turnClass;

    public bool isTurn = false;
    [SerializeField] private PlayerMovement _playerMovement;

    private GridSelect _gridSelect;
    
    // Start is called before the first frame update
    void Start()
    {
        // initMp = mp;
        
        turnSystem = GameObject.Find("Turn-Manager").GetComponent<TurnSystemScript>();
        _gridSelect = GameObject.Find("Grid").GetComponent<GridSelect>();
        
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
            if(Input.GetMouseButtonDown(0))
            {
                DoMoveAction();
                isTurn = false;
                turnClass.isTurn = isTurn;
                turnClass.wasTurnPrev = true;

                // Makes camera turn to activePlayer
                turnSystem.isSwitched = true;
            }
        }
    }
    
    private void DoMoveAction()
    {
        Vector3 pos = GetMouseWorldPos();
        _playerMovement.MovePlayer(pos, _gridSelect.GetGridLayout(), _gridSelect.GetPathNodesMap());
    }

    private Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}