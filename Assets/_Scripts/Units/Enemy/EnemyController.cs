using UnityEngine;

public class EnemyController : MonoBehaviour
{
   
    private EnemyMovement enemyMovement;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        enemyMovement.FindPlayer();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

   
    private void HandleMovement()
    {
        if (enemyMovement.PlayerPos != null)
        {
            enemyMovement.MoveTowards();
            enemyMovement.RotateToTarget();
        }
    }

    public void HandleDeathAnimationEnd()
    {
        EnemyPool.Instance.ReturnToPool(this);
    }






}
