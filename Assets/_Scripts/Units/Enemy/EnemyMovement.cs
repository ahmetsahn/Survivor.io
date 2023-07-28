using ScriptableObjectArchitecture;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    private Transform playerPos;
    private Vector3 direction;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        UIManager.OnBuffPanelActive += SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive += SetMovementSpeedDefault;
    }

    private void RemoveListeners()
    {
        UIManager.OnBuffPanelActive -= SetMovementSpeedZero;
        UIManager.OnBuffPanelDeactive -= SetMovementSpeedDefault;
    }

    private void Start()
    {
        FindPlayer();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void FindPlayer()
    {
        playerPos = GameObject.Find("Player").transform;
    }

    public void HandleMovement()
    {
        MoveTowards();
        RotateToTarget();
    }

    public void MoveTowards()
    {
        direction = (playerPos.transform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void RotateToTarget()
    {
        direction = (playerPos.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        rb.MoveRotation(targetRotation);
    }

    public void SetMovementSpeedZero()
    {
        moveSpeed = 0;
    }

    public void SetMovementSpeedDefault()
    {
        moveSpeed = 5;
    }


}
