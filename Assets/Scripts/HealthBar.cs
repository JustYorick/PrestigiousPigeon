using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChange += HealthSystem_OnHealthChange;
    }

    private void HealthSystem_OnHealthChange(object sender, System.EventArgs e)
    {
        transform.Find("HealthBar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1, 1);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
