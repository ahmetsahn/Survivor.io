using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IHealth
{

    [SerializeField] private FloatReference currentHealth;
    [SerializeField] private FloatReference maxHealth;

    private void Start()
    {
        SetHealth();
    }

    public void SetHealth()
    {
        currentHealth.Value = maxHealth.Value;
    }
    
    public void TakeDamage(float damage)
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
