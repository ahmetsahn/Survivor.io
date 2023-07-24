using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerHealth
{
    

    private readonly FloatReference currentHealth;
    private readonly FloatReference maxHealth;


    public PlayerHealth(FloatReference currentHealth,FloatReference maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }
    public void SetHealth()
    {
        currentHealth.Value = maxHealth.Value;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth.Value -= damage;
    }

    public void CheckHealth()
    {
        if (currentHealth.Value <= 0)
        {
            GameManager.Instance.ChangeState(GameStates.Lose);
        }
    }

    
}
