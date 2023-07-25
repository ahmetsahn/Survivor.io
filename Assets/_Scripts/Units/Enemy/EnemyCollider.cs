using UnityEngine;

public class EnemyCollider
{

    private readonly Transform enemyTransform;
    private readonly BoxCollider2D boxCollider;

    private readonly int damage;

    public EnemyCollider(Transform enemyTransform, BoxCollider2D boxCollider, int damage)
    {
        this.enemyTransform = enemyTransform;
        this.boxCollider = boxCollider;
        this.damage = damage;
    }


    public void Hit()
    {
        SpawnShootEffect();
    }

    public void HandleOnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemyCanHit hitable))
        {
            hitable.Hit();

            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                health.TakeDamage(damage);


                health.CheckHealth();

            }
        }
    }

    private void SpawnShootEffect()
    {
        var shootEffect = EnemyShootEffectPool.Instance.GetObject();
        shootEffect.transform.position = enemyTransform.position;
        shootEffect.gameObject.SetActive(true);
    }

   
    public void SetColliderDisable()
    {
        boxCollider.enabled = false;
    }

 

  
}
