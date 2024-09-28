using UnityEngine;

namespace Script.Runtime.EnemyModule.Model
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData")]
    public class EnemySo : ScriptableObject
    {
        public int Damage;
        public int MaxHealth;
        
        public float MovementSpeed;
        
        public GameObject EnemyDeathParticle;
    }
}