using ScriptableObjectArchitecture;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyCollider : MonoBehaviour, IPlayerCanHit
{
    private BoxCollider2D boxCollider;

    [SerializeField] private FloatReference damage;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
        SetEnableCollider();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemyCanHit hitable))
        {
            hitable.Hit();

            if (collision.gameObject.TryGetComponent(out IHealth health))
            {
                Debug.Log("Hit");
                health.TakeDamage(damage.Value);


                health.CheckHealth();

            }
        }
    }

    public void Hit()
    {
        SpawnShootEffect();
    }

    private void SpawnShootEffect()
    {
        var shootEffect = EnemyShootEffectPool.Instance.GetObject();
        shootEffect.transform.position = transform.position;
        shootEffect.gameObject.SetActive(true);
    }

   
    public void SetDisableCollider()
    {
        boxCollider.enabled = false;
    }

    public void SetEnableCollider()
    {
        boxCollider.enabled = true;
    }

    private void AddListeners()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnDeath += SetDisableCollider;
        }
    }

    private void RemoveListeners()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnDeath -= SetDisableCollider;
        }
    }


}
