using UnityEngine;

public class EnemyCollider : MonoBehaviour, IPlayerCanHit
{
   
    [field : SerializeField] public GameObject getShotEffect { get; set; }
    [SerializeField] private int damage;
    
    private void Start()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnDeath += SetColliderDisable;
        }
    }




    public void Hit()
    {
        SpawnShootEffect();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        shootEffect.transform.position = transform.position;
        shootEffect.gameObject.SetActive(true);
    }

    public void SetShootEffect(string shootEffectType)
    {
        EnemyShootEffectPool.Instance.SetPrefabType(shootEffectType);
    }

    public void SetColliderDisable()
    {
       GetComponent<Collider2D>().enabled = false;
    }

 

  
}
