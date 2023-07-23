using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack
{

    private readonly List<WeaponSO> weaponList;
    private readonly Transform bulletSpawnPos;
    private readonly PlayerAnimation playerAnimation;

    private WeaponSO currentWeapon;
    private float timeOfLastFiring = 0f;

    public PlayerAttack(List<WeaponSO> weaponList,Transform bulletSpawnPos, PlayerAnimation playerAnimation)
    {
        this.weaponList = weaponList;
        this.bulletSpawnPos = bulletSpawnPos;
        this.playerAnimation = playerAnimation;

        currentWeapon = weaponList[0];
    }

    public void SetWeapon(WeaponType weaponType)
    {
        var newWeapon = weaponList.Find(weapon => weapon.WeaponType == weaponType);
        if (newWeapon != null)
        {
            currentWeapon = newWeapon;
        }
    }

    protected void SpawnBullet()
    {
        var bullet = BulletPool.Instance.GetObject();
        bullet.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        bullet.gameObject.SetActive(true);
    }
    protected void SpawnMuzzleFlash()
    {
        var muzzleFlash = MuzzleFlashPool.Instance.GetObject();
        muzzleFlash.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        muzzleFlash.gameObject.SetActive(true);
    }

    public void FireControl()
    {
        if (Time.time >= timeOfLastFiring + currentWeapon.FireRate)
        {
            Shoot();
            timeOfLastFiring = Time.time;
        }

    }

    protected void Shoot()
    {
        playerAnimation.PlayShootAnimation();
        SpawnBullet();
        SpawnMuzzleFlash();
    }

}
