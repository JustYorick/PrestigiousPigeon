using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private Transform Character;
    private CharacterBattle playerCharacterBattle;
    private CharacterBattle enemyCharacterBattle;

    private static BattleHandler instance;

    public static BattleHandler GetInstance()
    {
        return instance;
    }
    
    void Start()
    {
        // spawn player & enemy characters
        playerCharacterBattle = SpawnCharachter(true);
        enemyCharacterBattle = SpawnCharachter(false);
    }
    
    void Update()
    {
        // start player attack on enemy character on spacebar press
        if (Input.GetKeyDown("space"))
        {
            playerCharacterBattle.Attack(enemyCharacterBattle);
        }

        if (Input.GetKeyDown("v"))
        {
            playerCharacterBattle.Heal(25);
        }
    }
    
    private CharacterBattle SpawnCharachter (bool isPlayerTeam)
    {
        Vector3 position;
        // set position based on player team
        if (isPlayerTeam)
        {
            position = new Vector3(-2, (float)0.4);
        }
        else
        {
            position = new Vector3(+2, (float)0.4);
        }
        // Create transform
        Transform characterTransform = Instantiate(Character, position, Quaternion.identity);
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayerTeam);

        return characterBattle;
    }
}
