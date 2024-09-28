using UnityEngine;

namespace Script.Runtime.BulletModule
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "Scriptable Object/BulletData")]
    public class BulletSo : ScriptableObject
    {
        public int Damage;
        
        public float MovementSpeed;
        
        public LayerMask TargetLayer;
    }
}