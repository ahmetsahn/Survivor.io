using UnityEngine;

public class PlayerMovement
{
    
    private readonly Rigidbody2D rb;
    private readonly Transform playerTransform;
    private readonly PlayerAnimation playerAnimation;
    private readonly IInput playerInput;

    private float currentMoveSpeed;
    private bool isMoving;
    private const int MinScaleAngle = 90;
    private const int MaxScaleAngle = 90;

    public PlayerMovement(Rigidbody2D rb,Transform playerTransform,PlayerAnimation playerAnimation, IInput playerInput, float currentMoveSpeed)
    {
        this.rb = rb;
        this.playerTransform = playerTransform;
        this.playerAnimation = playerAnimation;
        this.playerInput = playerInput;
        this.currentMoveSpeed = currentMoveSpeed;
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



    public void UpdateMovementState()
    {
        isMoving = (playerInput.HorizontalInput != 0 || playerInput.VerticalInput != 0);
    }
    
    

    private Quaternion CalculateBodyRotation()
    {
        var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerTransform.position;
        difference.Normalize();
        var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotZ);

    }

    public void SetBodyRotation()
    {
        playerAnimation.SetTopTorsoRotation(CalculateBodyRotation());
    }

    private Quaternion CalculateRotationForFeet()
    {
        var velocity = rb.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);

    }
    
    private void SetRotateFeet()
    {
        playerAnimation.SetBotTorsoRotation(CalculateRotationForFeet());
    }

    private void SetBotTorsoScaleBasedOnAngle()
    {
        float angle = playerAnimation.GetBotTorsoLocalEulerAnglesZ();

        if (angle > MinScaleAngle && angle < MaxScaleAngle)
        {
            playerAnimation.SetBotTorsoLocalScale(new Vector3(-1, -1, -1));
        }
        else
        {
            playerAnimation.SetBotTorsoLocalScale(new Vector3(1, 1, 1));
        }
    }

    public void HandleBotTorsoMovement()
    {
        if(isMoving)
        {
            SetRotateFeet();
            SetBotTorsoScaleBasedOnAngle();
        }

        playerAnimation.BotTorsoMoveAnimation(isMoving);




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
}
