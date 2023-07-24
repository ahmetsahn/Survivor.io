using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerAnimation
{

    private readonly Animator topTorso;
    private readonly Animator botTorso;
    private readonly Rigidbody2D rb;
    private readonly BoolReference isMoving;

    private const string IS_MOVING = "isMoving";
    private const string SHOOT = "Shoot";
  
    public PlayerAnimation(Animator topTorso,Animator botTorso,Rigidbody2D rb,BoolReference isMoving)
    {
        this.topTorso = topTorso;
        this.botTorso = botTorso;
        this.rb = rb;
        this.isMoving = isMoving;
    }

    public void PlayShootAnimation()
    {
        topTorso.Play(SHOOT);
    }

    public void SetTopTorsoRotation()
    {
   
        Vector3 topTorsoPosition = topTorso.transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        topTorso.transform.rotation = topTorsoPosition.CalculateAngleRotationBetweenPoints(mousePosition);

    }

    public void SetBotTorsoRotation()
    {
        botTorso.transform.rotation = rb.CalculateRotationFromVelocity();
    }

    public void BotTorsoMoveAnimation(bool move)
    {
        botTorso.SetBool(IS_MOVING, move);
    }

    

    public void HandleAnimatorsRotation()
    {
        SetTopTorsoRotation();
        BotTorsoMoveAnimation(isMoving.Value);
        SetBotTorsoRotation();
    }
}
