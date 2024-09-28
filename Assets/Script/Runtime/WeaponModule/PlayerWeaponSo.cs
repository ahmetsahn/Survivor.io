using Script.Ahmet.ObjectPool;
using Script.Runtime.BulletModule;
using UnityEngine;

namespace Script.Runtime.WeaponModule
{
    [CreateAssetMenu(fileName = "PlayerWeaponData", menuName = "Scriptable Object/PlayerWeapon")]
    public class PlayerWeaponSo : ScriptableObject
    {
        [SerializeField] 
        private GameObject bulletPrefab;
        
        public void Attack(Transform bulletSpawnPoint, int damage)
        {
            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().SetDamage(damage);
        }
    }
}