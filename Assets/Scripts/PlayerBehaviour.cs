using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject healthBar;

    void Start()
    {
        healthBar = GameObject.Find("HealthBarBack/HealthBarFront");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakeDmg(20);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerHeal(5);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        healthBar.transform.localScale = (new Vector3(
            GameManager.gameManager._playerHealth.HealthPercentage(GameManager.gameManager._playerHealth.Health),
            (float)1.010101, (float)1.010101));
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        healthBar.transform.localScale = (new Vector3(
            GameManager.gameManager._playerHealth.HealthPercentage(GameManager.gameManager._playerHealth.Health),
            (float)1.010101, (float)1.010101));
    }
}