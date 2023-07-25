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

    public void SetWeapon(WeaponSO weapon)
    {
        currentWeapon = weapon;
    }

   

    protected void SpawnBullet()
    {
        var bullet = PistolBulletPool.Instance.GetObject();
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
            OnShoot?.Invoke();
            timeOfLastFiring = Time.time;
        }

    }

    private void Shoot()
    {
        SpawnBullet();
        SpawnMuzzleFlash();
    }

}
