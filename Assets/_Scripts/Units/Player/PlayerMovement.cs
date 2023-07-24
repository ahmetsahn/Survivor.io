using ScriptableObjectArchitecture;
using System;
using UnityEngine;

public class PlayerMovement
{
    
    private readonly Rigidbody2D rb;
    private readonly IInput playerInput;
    private float currentMoveSpeed;
    private BoolReference isMoving;
    
    public PlayerMovement(Rigidbody2D rb, IInput playerInput, float currentMoveSpeed,BoolReference isMoving)
    {
        this.rb = rb;
        this.playerInput = playerInput;
        this.currentMoveSpeed = currentMoveSpeed;
        this.isMoving = isMoving;
    }
    public void AddListener()
    {
        UIManager.OnBuffPanelActive += SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive += SetMovementSpeedDefault;
    }

    public void RemoveListener()
    {
        UIManager.OnBuffPanelActive -= SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive -= SetMovementSpeedDefault;
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
