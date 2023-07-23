using ScriptableObjectArchitecture;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth
{
    
    public event Action OnHit;
    public event Action OnDeath;

    private float health;
    [SerializeField] private FloatReference currentExp;

    public EnemyHealth(float health, FloatReference currentExp)
    {
        this.health = health;
        this.currentExp = currentExp;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        OnHit?.Invoke();
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            OnDeath?.Invoke();
            currentExp.Value++;
        }
    }
    
    
}
