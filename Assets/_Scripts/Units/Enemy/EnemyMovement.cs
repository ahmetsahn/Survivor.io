using ScriptableObjectArchitecture;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMovement
{
    private readonly Rigidbody2D rb;
    private readonly Transform enemyTransform;

    private float moveSpeed;
    private Vector3 direction;
    private GameObject playerPos;

    public EnemyMovement(Rigidbody2D rb, Transform enemyTransform,float moveSpeed)
    {
        this.rb = rb;
        this.enemyTransform = enemyTransform;
        this.moveSpeed = moveSpeed;
    }

    public void FindPlayer()
    {
        playerPos = GameObject.Find("Player");
    }

    public void MoveTowards()
    {
        direction = (playerPos.transform.position - enemyTransform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void RotateToTarget()
    {
        direction = (playerPos.transform.position - enemyTransform.position).normalized;
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
