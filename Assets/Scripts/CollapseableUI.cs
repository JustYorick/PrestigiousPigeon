using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CollapseableUI : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTurnUI;
    [SerializeField] private GameObject EnemyTurnUI;

    public void ShowPlayerTurnUI()
    {
        StartCoroutine(MoveUpAndDown(170, PlayerTurnUI));
    }
    
    public void ShowEnemyTurnUI()
    {
        StartCoroutine(MoveUpAndDown(170, EnemyTurnUI));
    }

    IEnumerator MoveUpAndDown(int distance, GameObject uiElement)
    {
        yield return MoveUI(distance, uiElement);
        yield return new WaitForSeconds(1);
        yield return MoveUI(-distance, uiElement);
    }
    
    private IEnumerator MoveUI (int distance, GameObject uiElement)
    {
        int amountPerMove = 5;
        if (distance < 0) { amountPerMove = -5; }
        int moved = 0;
         
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
