using ScriptableObjectArchitecture;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IHealth
{
    
    public event Action OnHit;
    public event Action OnDeath;

    [SerializeField] private float currentHealth;

    [SerializeField] private FloatReference currentExp;

    public void SetHealth()
    {

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
    
    
}
