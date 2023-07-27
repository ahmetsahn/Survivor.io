using ScriptableObjectArchitecture;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour,IHealth,IPlayerCanHit
{
   
    private EnemyMovement enemyMovement;
    private EnemyAudio enemyAudio;
    private EnemyHealth enemyHealth;
    private EnemyAnimation enemyAnimation;
    private EnemyCollider enemyCollider;
    private EnemySpriteRenderer enemySpriteRenderer;


    [Header("Health")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [Header("Audio")]
    [SerializeField] private AudioClip[] audioClips;
    

    [Header("UI Referance")]
    [SerializeField] private FloatReference currentExp;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Attack")]
    [SerializeField] private int damage;

    
    

    #region Components

    private AudioSource audioSource;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        enemyAudio = new EnemyAudio(audioSource,audioClips);
        enemyMovement = new EnemyMovement(rb,transform,moveSpeed);
        enemyHealth = new EnemyHealth(currentHealth, maxHealth, currentExp);
        enemyAnimation = new EnemyAnimation(animator);
        enemyCollider = new EnemyCollider(transform,boxCollider,damage);
        enemySpriteRenderer = new EnemySpriteRenderer(spriteRenderer);
    }

    private void OnEnable()
    {
        AddListener();
        
    }

    private void OnDisable()
    {
        RemoveListener();
        enemyCollider.SetDefaultCollider();
        enemySpriteRenderer.SetDefaultSpriteAlpha();
        enemyHealth.SetDefaulthHealth();
    }

    private void Start()
    {
        enemyMovement.FindPlayer();
    }

    private void FixedUpdate()
    {
        enemyMovement.HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyCollider.HandleOnTriggerEnter2D(collision);
    }

    public void HandleDeathAnimationEnd()
    {
        EnemyPool.Instance.ReturnToPool(this);
    }

    public void TakeDamage(int damage)
    {
        enemyHealth.TakeDamage(damage);
    }

    public void CheckHealth()
    {
        enemyHealth.CheckHealth();
    }

    public void Hit()
    {
        enemyCollider.Hit();
    }

    private void AddListener()
    {
        enemyHealth.OnHit += enemyAudio.PlayHitClip;
        enemyHealth.OnDeath += enemyAnimation.PlayDeathAnimation;
        enemyHealth.OnDeath += enemyCollider.SetColliderDisable;
        UIManager.OnBuffPanelActive += enemyMovement.SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive += enemyMovement.SetMovementSpeedDefault;
    }

    private void RemoveListener()
    {
        enemyHealth.OnHit -= enemyAudio.PlayHitClip;
        enemyHealth.OnDeath -= enemyAnimation.PlayDeathAnimation;
        enemyHealth.OnDeath -= enemyCollider.SetColliderDisable;
        UIManager.OnBuffPanelActive -= enemyMovement.SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive -= enemyMovement.SetMovementSpeedDefault;
    }
}
