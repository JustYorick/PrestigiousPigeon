using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class ManaSystem : MonoBehaviour
{
    [SerializeField ] private int manaPointsPerTurn = 5;
    [SerializeField ] private int manaPointsLeft;
    private TMPro.TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the text component to display the amount of mana left
        text = GetComponent<TMPro.TMP_Text>();

        // Start the first turn
        StartTurn();
    }

    public void StartTurn(){
        // Fill the mana back to the max
        manaPointsLeft = manaPointsPerTurn;

        // Update the text
        UpdateText();
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

        // Update the text
        UpdateText();
    }

    // Return the amount of mana left
    public int GetMana() => manaPointsLeft;

    // Update manapoints text
    void UpdateText(){
        text.SetText(manaPointsLeft.ToString());
    }
}
