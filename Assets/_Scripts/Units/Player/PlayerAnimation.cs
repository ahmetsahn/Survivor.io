using ScriptableObjectArchitecture;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator topTorso;
    [SerializeField] private Animator botTorso;
    [SerializeField] private BoolReference isMoving;
   

    private const string IS_MOVING = "isMoving";
    private const string SHOOT = "Shoot";

    private void OnEnable()
    {
        AddListeners();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.state == GameStates.Pause) return;
        HandleAnimatorsRotation();
    }

    public void PlayShootAnimation()
    {
        topTorso.Play(SHOOT);
    }

    public void SetTopTorsoRotation()
    { 
        topTorso.transform.LookAtMouse();
    }

    public void SetBotTorsoRotation()
    {
        botTorso.transform.RotateFromMovementDirection();
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

    private void AddListeners()
    {
        if(TryGetComponent(out PlayerAttack playerAttack))
        {
            playerAttack.OnShoot += PlayShootAnimation;
        }
    }

}
