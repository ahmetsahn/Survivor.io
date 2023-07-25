using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class BaseBullet : MonoBehaviour, IDeathTimer
{
    [SerializeField] protected BulletStatsSO bulletStats;
    [field: SerializeField] public float DeathTimer { get; set; }

  
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out IPlayerCanHit hitable))
        {
            hitable?.Hit();

            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                health?.TakeDamage(bulletStats.Damage);


                health?.CheckHealth();

            }
        }


    }
}
