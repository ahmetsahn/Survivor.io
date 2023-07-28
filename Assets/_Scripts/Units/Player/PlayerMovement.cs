using ScriptableObjectArchitecture;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private IInput playerInput;
    [SerializeField] private BoolReference isMoving;
    [SerializeField] private float currentMoveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<IInput>();
    }

    private void Update()
    {
        UpdateIsMove();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.state == GameStates.Pause) return;
        HandleMove();
    }

    public void HandleMove()
    {
        ApplyMovementForce();
    }

    public void ApplyMovementForce()
    {
        rb.AddForce(new Vector2(playerInput.HorizontalInput, playerInput.VerticalInput) * currentMoveSpeed);
    }

    public void SetMovementSpeedZero()
    {
        currentMoveSpeed = 0;
    }

    public void SetMovementSpeedDefault()
    {
        currentMoveSpeed = 50;
    }

    public void UpdateIsMove()
    {
        isMoving.Value = (playerInput.HorizontalInput != 0 || playerInput.VerticalInput != 0);
    }

  
}
