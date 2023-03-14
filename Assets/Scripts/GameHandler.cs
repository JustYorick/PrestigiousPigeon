using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Transform HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        Transform healthBarTransform = Instantiate(HealthBar, new Vector3(0, 1), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
        
        Debug.Log("Health: " + healthSystem.GetHealth());
        healthSystem.Damage(50);
        Debug.Log("Health: " + healthSystem.GetHealthPercent());
        healthSystem.Heal(5);
        Debug.Log("Health: " + healthSystem.GetHealth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
