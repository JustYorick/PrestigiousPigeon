using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
    [SerializeField ] private int manaPointsPerTurn = 5;
    [SerializeField ] private int manaPointsLeft;

    // Start is called before the first frame update
    void Start()
    {
        StartTurn();
    }

    void StartTurn(){
        // Fill the mana back to the max
        manaPointsLeft = manaPointsPerTurn;
    }

    public void UseMana(int amount){
        // Throw an exception if the player tries to use more mana than they have left
        if(amount > manaPointsLeft){
            throw new System.ArgumentException(
                string.Format("Not enough mana to perform action, {0} used while only {1} left.", amount, manaPointsLeft)
            );
        }

        // Subtract the amount of mana being used from the amount of mana left
        manaPointsLeft -= amount;
    }

    // Return the amount of mana left
    public int GetMana() => manaPointsLeft;
}
