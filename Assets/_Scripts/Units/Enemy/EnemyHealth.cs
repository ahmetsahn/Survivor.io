using ScriptableObjectArchitecture;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth
{
    
    public event Action OnHit;
    public event Action OnDeath;

    private float currentHealth;
    private readonly float maxHealth;
    [SerializeField] private FloatReference currentExp;

    public EnemyHealth(float currentHealth,float maxHealth, FloatReference currentExp)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.currentExp = currentExp;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHit?.Invoke();
    }

    public void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
            currentExp.Value++;
        }
    }

    public void SetDefaulthHealth()
    {
        currentHealth = maxHealth;
    }   
    
    
}
