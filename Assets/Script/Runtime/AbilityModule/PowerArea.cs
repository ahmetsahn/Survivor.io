using Script.Runtime.Interface;
using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

namespace Assets.Script.Runtime.AbilityModule
{
    public class PowerArea : MonoBehaviour
    {
        [SerializeField]
        private LayerMask enemyLayer;

        private float _radius;
        
        private int _damage;

        void Start()
        {
            ApplyAreaDamage().Forget();
        }

        public void Initialize(float radius, int damage)
        {
            _radius = radius;
            _damage = damage;
            var particleScale = _radius * 3 / 7;
            Vector3 targetScale = new Vector3(particleScale, particleScale, particleScale);
            transform.DOScale(targetScale, 0.25f);
        }

        private async UniTask ApplyAreaDamage()
        {
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                    transform.position,
                    _radius,
                    enemyLayer);

                foreach (var hitEnemy in hitEnemies)
                {
                    if (!hitEnemy.TryGetComponent(out IHealth health)) continue;

                    if (health != GetComponent<IHealth>())
                    {
                        health.OnTakeDamageEvent?.Invoke(_damage);
                    }
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            }
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, _radius);
        }
    }
}