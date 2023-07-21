using ScriptableObjectArchitecture;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float currentMoveSpeed;
    private Vector3 direction;
    private Rigidbody2D rb;

    [HideInInspector] public GameObject PlayerPos { get ; private set; }


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

    public void FindPlayer()
    {
        PlayerPos = GameObject.Find("Player");
    }

    public void MoveTowards()
    {
        direction = (PlayerPos.transform.position - transform.position).normalized;
        rb.velocity = direction * currentMoveSpeed;
    }

    public void RotateToTarget()
    {
        direction = (PlayerPos.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        rb.MoveRotation(targetRotation);
    }

    public void SetMovementSpeedZero()
    {
        currentMoveSpeed = 0;
    }

    public void SetMovementSpeedDefault()
    {
        currentMoveSpeed = 5;
    }




}
