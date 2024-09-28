using Script.Ahmet.ObjectPool;
using Script.Runtime.Interface;
using UnityEngine;

namespace Script.Runtime.BulletModule
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] 
        private BulletSo data;
        
        private int _damage;

        private void Awake()
        {
            SetDefaultData();
        }

        private void SetDefaultData()
        {
            _damage = data.Damage;
        }

        public void SetDamage(int playerDamage)
        {
            _damage += playerDamage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((data.TargetLayer & 1 << other.gameObject.layer) == 0)
            {
                return;
            }
            
            if (other.TryGetComponent(out IHealth health))
            {
                health.OnTakeDamageEvent?.Invoke(_damage);
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
        }

        private void Update()
        {
            transform.Translate(Vector2.right * (data.MovementSpeed * Time.deltaTime));
        }

        private void OnDisable()
        {
            SetDefaultData();
        }
    }
}