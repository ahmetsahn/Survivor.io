using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        
    }

    private void Start()
    {
        playerHealth.SetHealth();
    }


    private void Update()
    {
        if (GameManager.Instance.state == GameStates.Pause) return;

        playerInput.GetInput();
        playerMovement.UpdateMovementState(playerInput);
        playerMovement.SetBodyRotation(playerAnimation);
        playerMovement.HandleBotTorsoMovement(playerAnimation);
        playerAttack.FireControl(playerAnimation, playerAttack.BulletSpawnPos);
    }

    

    private void FixedUpdate()
    {
        playerMovement.ApplyMovementForce(playerInput);
    }

    

    
}
