using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack
{

    private readonly List<WeaponSO> weaponList;
    private readonly Transform bulletSpawnPos;
   
    private WeaponSO currentWeapon;
    private float timeOfLastFiring = 0f;

    public event Action OnShoot;

  
    public PlayerAttack(List<WeaponSO> weaponList,Transform bulletSpawnPos)
    {
        this.weaponList = weaponList;
        this.bulletSpawnPos = bulletSpawnPos;
       

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

    private void Shoot()
    {
        OnShoot?.Invoke();
        SpawnBullet();
        SpawnMuzzleFlash();
    }

}
