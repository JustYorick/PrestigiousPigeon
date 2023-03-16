using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject kingHealthBar;
    public GameObject viking1HealthBar;
    public GameObject viking2HealthBar;
    public GameObject viking3HealthBar;

    void Start()
    {
        kingHealthBar = GameObject.Find("KingHealthBar/HealthBarFront");
        viking1HealthBar = GameObject.Find("Viking1HealthBar/HealthBarFront");
        viking2HealthBar = GameObject.Find("Viking2HealthBar/HealthBarFront");
        viking3HealthBar = GameObject.Find("Viking3HealthBar/HealthBarFront");
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
        kingHealthBar.transform.localScale = (new Vector3(
            GameManager.gameManager._playerHealth.HealthPercentage(GameManager.gameManager._playerHealth.Health),
            (float)1.010101, (float)1.010101));
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        kingHealthBar.transform.localScale = (new Vector3(
            GameManager.gameManager._playerHealth.HealthPercentage(GameManager.gameManager._playerHealth.Health),
            (float)1.010101, (float)1.010101));
    }
}