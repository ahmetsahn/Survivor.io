using System;
using Cysharp.Threading.Tasks;
using Script.Runtime.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Runtime.PlayerModule.View
{
    public class PlayerView : MonoBehaviour, IHealth
    {
        public Animator Animator;

        public Image HealthBar;
        
        public Transform AimTransform;
        public Transform BodyTransform;
        
        public SpriteRenderer[] HoodRenderers;
        public SpriteRenderer[] ArmorRenderers;
        public SpriteRenderer[] WeaponRenderers;
        public SpriteRenderer[] ShoesRenderers;
        public Action<int> OnTakeDamageEvent { get; set; }

        public Action<Vector2> OnMoveEvent;
        
        public Action<Collider2D> OnPlayerCollisionEvent;
        
        public event Action OnStartAttackEvent;

        private void Start()
        {
            OnStartAttackEvent?.Invoke();
        }

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            OnMoveEvent?.Invoke(new Vector2(horizontal, vertical));
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnPlayerCollisionEvent?.Invoke(other);
        }
    }
}