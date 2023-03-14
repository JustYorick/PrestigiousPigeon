using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    public void Setup(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            BattleHandler.GetInstance();
            Debug.Log("Hey, it's you!");
        }
        else
        {
            BattleHandler.GetInstance();
            Debug.Log("Hey, it's an enemy!");
        }
    }
    
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
    public void Attack(CharacterBattle targetCharacterBattle)
    {
        // get attack direction, (for animation)
        Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
        Debug.Log("He attacked in this direction: " + attackDir);
    }
}
