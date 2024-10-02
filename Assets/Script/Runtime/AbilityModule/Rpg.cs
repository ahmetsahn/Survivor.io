using Script.Ahmet.ObjectPool;
using Script.Runtime.Interface;
using UnityEngine;

namespace Script.Runtime.AbilityModule
{
    public class Rpg : MonoBehaviour
    {
        [SerializeField]
        private LayerMask targetLayer;
        
        [SerializeField]
        private GameObject explosionEffectPrefab;
        
        private Vector3 _direction;

        private float _movementSpeed;
        private float _radius;
        
        private int _damage;
        
        public void Initialize(float movementSpeed, float radius, int damage)
        {
            SetRandomDirection();
            _movementSpeed = movementSpeed;
            _radius = radius;
            _damage = damage;
        }
        
        private void SetRandomDirection()
        {
            _direction = Random.insideUnitCircle.normalized;
        }

        private void Update()
        {
            transform.Translate(_direction * (_movementSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((targetLayer & 1 << other.gameObject.layer) == 0)
            {
                return;
            }
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                transform.position, 
                _radius,
                targetLayer);
                
            foreach (var hitEnemy in hitEnemies)
            {
                if (!hitEnemy.TryGetComponent(out IHealth health)) continue;
                
                if (health != GetComponent<IHealth>())
                {
                    health.OnTakeDamageEvent?.Invoke(_damage);
                }
            }

            ObjectPoolManager.SpawnObject(explosionEffectPrefab, transform.position, Quaternion.identity);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}