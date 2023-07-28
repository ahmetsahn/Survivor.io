using ScriptableObjectArchitecture;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerCollider playerCollider;
    private IInput playerInput;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();
        playerInput = GetComponent<IInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<PlayerCollider>();
    }

   
    private void Start()
    {
        playerHealth.SetHealth();
        playerAttack.SetStartWeapon();
    }


    private void Update()
    {
        if (GameManager.Instance.state == GameStates.Pause) return;

        playerInput.ReadInput();
        playerAttack.FireControl();
        playerMovement.UpdateIsMove();
    }

    

    private void FixedUpdate()
    {
        playerMovement.HandleMove();
        playerAnimation.HandleAnimatorsRotation();
    }

    
}
