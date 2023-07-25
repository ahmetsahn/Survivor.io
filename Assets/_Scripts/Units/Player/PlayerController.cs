using ScriptableObjectArchitecture;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,IEnemyCanHit,IHealth
{
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    private PlayerCollider playerCollider;
    private IInput playerInput;

    [Header("Health")]
    [SerializeField] private FloatReference currentHealth;
    [SerializeField] private FloatReference maxHealth;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private BoolReference isMoving;

    [Header("Weapon")]
    [SerializeField] private List<WeaponSO> weaponList;
    [SerializeField] private Transform bulletSpawnPos;

    [Header("Animation")]
    [SerializeField] private Animator topTorso;
    [SerializeField] private Animator botTorso;

    #region Components


    Rigidbody2D rb;
    

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnimation = new PlayerAnimation(topTorso, botTorso, rb, isMoving);
        playerAttack = new PlayerAttack(weaponList, bulletSpawnPos);
        playerHealth = new PlayerHealth(currentHealth, maxHealth);
        playerInput = new PlayerKeyboardInput();
        playerMovement = new PlayerMovement(rb, playerInput, moveSpeed, isMoving);
        playerCollider = new PlayerCollider(transform);

    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void Start()
    {
        playerHealth.SetHealth();
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

    public void Hit()
    {
        playerCollider.Hit();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public void CheckHealth()
    {
        playerHealth.CheckHealth();
    }

    public void AddListeners()
    {
        UIManager.Instance.OnBuffPanelActive += playerMovement.SetMovementSpeedZero;
        UIManager.Instance.OnBuffPanelDeactive += playerMovement.SetMovementSpeedDefault;
        playerAttack.OnShoot += playerAnimation.PlayShootAnimation;
    }

    public void RemoveListeners()
    {
        UIManager.Instance.OnBuffPanelActive -= playerMovement.SetMovementSpeedZero;
        UIManager.Instance.OnBuffPanelDeactive -= playerMovement.SetMovementSpeedDefault;
        playerAttack.OnShoot -= playerAnimation.PlayShootAnimation;
    }
}
