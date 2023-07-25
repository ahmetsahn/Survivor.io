using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Electric")]
public class ElectricWeapon : BaseWeapon
{
    private void Awake()
    {
        weaponType = WeaponType.Electric;
    }
    public override void SpawnBullet(Transform bulletSpawnPos)
    {
        var bullet = ElectricBulletPool.Instance.GetObject();
        bullet.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        bullet.gameObject.SetActive(true);
    }
    public override void SpawnMuzzleFlash(Transform bulletSpawnPos)
    {
        var muzzleFlash = MuzzleFlashPool.Instance.GetObject();
        muzzleFlash.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        muzzleFlash.gameObject.SetActive(true);
    }
}
