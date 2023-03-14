using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private HealthSystem healthSystem;
    public void Setup(bool isPlayerTeam)
    {
        // check team
        if (isPlayerTeam)
        {
            // spawn player
            BattleHandler.GetInstance();
            Debug.Log("Hey, it's you!");
        }
        else
        {
            // spawn enemy
            BattleHandler.GetInstance();
            Debug.Log("Hey, it's an enemy!");
        }

        healthSystem = new HealthSystem(100);
    }
    
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
    public void Attack(CharacterBattle targetCharacterBattle)
    {
        // get attack direction
        Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
        Damage(5);
        Debug.Log("New Health" + healthSystem.GetHealth());
        Debug.Log("He attacked in this direction: " + attackDir + "dealing 5 damage!");
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
        Debug.Log("Wow he healed for " + healAmount);
        Debug.Log("New Health: " + healthSystem.GetHealth());
    }

    public void Damage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
        Debug.Log("Holy moly! He attacked dealing " + damageAmount + " damage");
    }
}
