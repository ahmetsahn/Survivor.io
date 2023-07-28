using ScriptableObjectArchitecture;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour,IHealth
{
    
    public event Action OnHit;
    public event Action OnDeath;

    private float health;
    [SerializeField] private FloatReference maxHealth;
    [SerializeField] private FloatReference currentExp;

    private void OnDisable()
    {
        health = maxHealth.Value;
    }

    private void Start()
    {
        health = maxHealth.Value;
    }

    public void TakeDamage(float damage)
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
