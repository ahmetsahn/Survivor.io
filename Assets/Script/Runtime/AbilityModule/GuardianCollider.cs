using Script.Runtime.Interface;
using UnityEngine;
using System;

namespace Assets.Script.Runtime.AbilityModule
{
    public class GuardianCollider : MonoBehaviour
    {
        [SerializeField]
        private LayerMask targetLayer;

        private int _damage;

        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((targetLayer & 1 << other.gameObject.layer) == 0)
            {
                return;
            }

            if (other.TryGetComponent(out IHealth health))
            {
                health.OnTakeDamageEvent?.Invoke(_damage);
            }
        }
    }
}