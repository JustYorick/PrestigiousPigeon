using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public Transform HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        // set max health
        HealthSystem healthSystem = new HealthSystem(100);
        // create HealthBar
        Transform healthBarTransform = Instantiate(HealthBar, new Vector3(0, 1), Quaternion.identity);
        // get healthbar Object
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
