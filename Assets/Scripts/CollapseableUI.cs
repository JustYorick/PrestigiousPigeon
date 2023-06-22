using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseableUI : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTurnUI;
    [SerializeField] private GameObject EnemyTurnUI;
    [SerializeField] private GameObject ObjectiveUI;
    [SerializeField] private Canvas canvas;
    private Vector3 PlayerTurnUIDefaultPos;
    private Vector3 EnemyTurnUIDefaultPos;
    private Vector3 ObjectiveUIDefaultPos;

    private void Start()
    {
        PlayerTurnUIDefaultPos = PlayerTurnUI.transform.position;
        EnemyTurnUIDefaultPos = EnemyTurnUI.transform.position;
        ObjectiveUIDefaultPos = ObjectiveUI.transform.position;
        ShowObjectiveUI();
    }

    public void ShowPlayerTurnUI()
    {
        StopCoroutine(MoveUpAndDown(170 * canvas.scaleFactor, PlayerTurnUI));
        PlayerTurnUI.transform.position = PlayerTurnUIDefaultPos;
        Coroutine a = StartCoroutine(MoveUpAndDown(170 * canvas.scaleFactor, PlayerTurnUI));
    }
    
    public void ShowEnemyTurnUI()
    {
        StopCoroutine(MoveUpAndDown(170 * canvas.scaleFactor, EnemyTurnUI));
        EnemyTurnUI.transform.position = EnemyTurnUIDefaultPos;
         Coroutine b = StartCoroutine(MoveUpAndDown(170 * canvas.scaleFactor, EnemyTurnUI));
    }

    public void ShowObjectiveUI()
    {
        StartCoroutine(MoveUpAndDown(360 * canvas.scaleFactor, ObjectiveUI));
        ObjectiveUI.transform.position = ObjectiveUIDefaultPos;
    }
    
    IEnumerator MoveUpAndDown(float distance, GameObject uiElement)
    {
        yield return MoveUI(distance, uiElement);
        yield return new WaitForSeconds(1);
        yield return MoveUI(-distance, uiElement);
        if(uiElement == ObjectiveUI){
            ReDesign.TurnController.ResolveNextTurn();
        }
    }
    
    private IEnumerator MoveUI (float distance, GameObject uiElement)
    {
        float amountPerMove = 5 * canvas.scaleFactor;
        if (distance < 0) { amountPerMove = -5 * canvas.scaleFactor; }
        float moved = 0;
         
        while (moved < Mathf.Abs(distance))
        {
            var position = uiElement.transform.position;
            position = new Vector3(position.x, position.y-amountPerMove, position.z);
            uiElement.transform.position = position;
            moved += Mathf.Abs(amountPerMove);
            yield return null;
        }
    }
}