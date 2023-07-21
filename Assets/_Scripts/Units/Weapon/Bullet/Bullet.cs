using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour,IDeathTimer
{
    [SerializeField] BulletStatsSO bulletStats;
    [field: SerializeField] public float DeathTimer { get; set; }
    [field: SerializeField] public BulletType BulletType { get; set; }


    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolTimer());
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * (bulletStats.Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.TryGetComponent(out IPlayerCanHit hitable))
        {
            hitable?.Hit();
            
            if(collision.gameObject.TryGetComponent(out IHealth health))
            {
                health?.TakeDamage(bulletStats.Damage);

                
                    health?.CheckHealthAndPlayDeathAnimation();
                
                
            }

            BulletPool.Instance.ReturnToPool(this);
        }


    }

    public IEnumerator ReturnToPoolTimer()
    {
        yield return new WaitForSeconds(DeathTimer);
        BulletPool.Instance.ReturnToPool(this);
    }
    
}
