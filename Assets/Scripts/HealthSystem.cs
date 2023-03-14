using System;

public class HealthSystem
{
    public event EventHandler OnHealthChange;
    private int health;
    private int healthMax;
    
    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health < 0) health = 0;
        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        if (OnHealthChange != null) OnHealthChange(this, EventArgs.Empty);
    }
}
