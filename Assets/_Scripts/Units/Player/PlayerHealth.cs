using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    

    [SerializeField] private FloatReference currentHealth;
    [SerializeField] private FloatReference maxHealth;

 
    public void SetHealth()
    {
        currentHealth.Value = maxHealth.Value;
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth.Value -= damage;
       
    }

    public void CheckHealthAndPlayDeathAnimation()
    {
        if (currentHealth.Value <= 0)
        {
            
            GameManager.Instance.ChangeState(GameStates.Lose);
        }
    }

    
}
