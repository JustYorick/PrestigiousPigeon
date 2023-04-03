using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    // Fields
    private int _currentHealth;
    private int _currentMaxHealth;
    public delegate void HealthChangedHandler(object source, float oldHealth, float newHealth);
    public event HealthChangedHandler OnHealthChanged; 
    
    
    // Properties
    public int Health { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHealth { get => _currentMaxHealth; set => _currentMaxHealth = value; }

    // Constructor

    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    // Methods
    // public void DmgUnit(int dmgAmount)
    // {
    //     if (_currentHealth > 0)
    //     {
    //         _currentHealth -= dmgAmount;
    //     }
    // }
    //
    // public void HealUnit(int healAmount)
    // {
    //     if (_currentHealth < _currentMaxHealth)
    //     {
    //         _currentHealth += healAmount;
    //     }
    //
    //     if (_currentHealth > _currentMaxHealth)
    //     {
    //         _currentHealth = _currentMaxHealth;
    //     }
    // }

    public float HealthPercentage(int health)
    {
        return (float)health / _currentMaxHealth;
    }

    public void ChangeHealth(int amount)
    {
        int oldHealth = _currentHealth;
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _currentMaxHealth);
        OnHealthChanged?.Invoke(this, oldHealth, _currentHealth);
    }
}