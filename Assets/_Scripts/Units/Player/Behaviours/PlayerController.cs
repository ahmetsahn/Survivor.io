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

    [Header("Health Settings")]
    [SerializeField] private FloatReference currentHealth;
    [SerializeField] private FloatReference maxHealth;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;

    [Header("Weapon Settings")]
    [SerializeField] private List<WeaponSO> weaponList;
    [SerializeField] private Transform bulletSpawnPos;

    [Header("Animation Settings")]
    [SerializeField] private Animator topTorso;
    [SerializeField] private Animator botTorso;

    #region Components


    Rigidbody2D rb;
    

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        playerAnimation = new PlayerAnimation(topTorso, botTorso);
        playerAttack = new PlayerAttack(weaponList,bulletSpawnPos,playerAnimation);
        playerHealth = new PlayerHealth(currentHealth,maxHealth);
        playerInput = new PlayerKeyboardInput();
        playerMovement = new PlayerMovement(rb, transform, playerAnimation, playerInput, moveSpeed);
        playerCollider = new PlayerCollider(transform);

        
    }

    private void OnEnable()
    {
        playerMovement.AddListener();
       
    }

    private void OnDisable()
    {
        playerMovement.RemoveListener();
       
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
    }

    

    private void FixedUpdate()
    {
        playerMovement.HandleMove();
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

    public void SetHealth()
    {
        playerHealth.SetHealth();
    }
}
