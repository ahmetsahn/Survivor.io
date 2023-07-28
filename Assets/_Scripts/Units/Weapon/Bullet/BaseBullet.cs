using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class BaseBullet : MonoBehaviour, IDeathTimer
{
    [SerializeField] protected FloatReference damage;
    [SerializeField] protected FloatReference speed;
    [field: SerializeField] public float DeathTimer { get; set; }


    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        transform.position += transform.right * (speed.Value * Time.deltaTime);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out IPlayerCanHit hitable))
        {
            hitable?.Hit();

            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                health?.TakeDamage(damage.Value);


                health?.CheckHealth();

            }
        }


    }
}
