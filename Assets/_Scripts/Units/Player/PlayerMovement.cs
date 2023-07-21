using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float currentMoveSpeed;
    private Rigidbody2D rb;

    private bool isMoving;
    private const int MinScaleAngle = 90;
    private const int MaxScaleAngle = 90;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        UIManager.OnBuffPanelActive += SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive += SetMovementSpeedDefault;
    }

    private void OnDisable()
    {
        UIManager.OnBuffPanelActive -= SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive -= SetMovementSpeedDefault;
    }


    public void UpdateMovementState(PlayerInput playerInput)
    {
        isMoving = (playerInput.Horizontal != 0 || playerInput.Vertical != 0);
    }
    
    

    private Quaternion CalculateBodyRotation()
    {
        var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, rotZ);

    }

    public void SetBodyRotation(PlayerAnimation playerAnimation)
    {
        playerAnimation.TopTorso.transform.rotation = CalculateBodyRotation();
    }

    private Quaternion CalculateRotationForFeet()
    {
        var velocity = rb.velocity;
        var angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);

    }
    
    private void SetRotateFeet(PlayerAnimation playerAnimation)
    {
        playerAnimation.BotTorso.transform.rotation = CalculateRotationForFeet();
    }

    private void SetBotTorsoScaleBasedOnAngle(PlayerAnimation playerAnimation)
    {
        float angle = playerAnimation.BotTorso.transform.localEulerAngles.z;

        if (angle > MinScaleAngle && angle < MaxScaleAngle)
        {
            playerAnimation.BotTorso.transform.localScale = new Vector3(-1, -1, -1);
        }
        else
        {
            playerAnimation.BotTorso.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void HandleBotTorsoMovement(PlayerAnimation playerAnimation)
    {
        if(isMoving)
        {
            playerAnimation.BotTorso.SetBool("isMoving", isMoving);
            SetRotateFeet(playerAnimation);
            SetBotTorsoScaleBasedOnAngle(playerAnimation);
        }

        else
        {
            playerAnimation.BotTorso.SetBool("isMoving", isMoving);
        }
        
        
       
       
    }
    
    public void ApplyMovementForce(PlayerInput playerInput)
    {
        rb.AddForce(new Vector2(playerInput.Horizontal, playerInput.Vertical) * currentMoveSpeed);
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
