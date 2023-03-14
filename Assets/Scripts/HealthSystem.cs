using System;

public class HealthSystem
{
    public event EventHandler OnHealthChange;
    private int health;
    private int healthMax;
    
    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        // set health to max health
        health = healthMax;
    }

    // get current health
    public int GetHealth()
    {
        return health;
    }

    // get current health percentage
    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }
    
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        // check if health is not 0
        if (health < 0) health = 0;
        // subscribe to OnHealthChange
        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        // check if health is not max
        if (health > healthMax) health = healthMax;
        // subscribe to OnHealthChange
        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }
}
