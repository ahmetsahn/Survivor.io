using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack
{
    private List<BaseWeapon> weapons;
    private BaseWeapon currentWeapon;
    private readonly Transform bulletSpawnPos;
    private float timeOfLastFiring = 0f;

    public event Action OnShoot;

  
    public PlayerAttack(List<BaseWeapon> weapons, Transform bulletSpawnPos)
    {
        this.weapons = weapons;
        this.bulletSpawnPos = bulletSpawnPos;
      
        currentWeapon = weapons.Find(x => x.weaponType == WeaponType.Pistol);
    }

    public void SetElectricWeapon()
    {
        currentWeapon = weapons.Find(x => x.weaponType == WeaponType.Electric);
    }


    public void FireControl()
    {
        if (Time.time >= timeOfLastFiring + currentWeapon.fireRate)
        {
            Shoot();
            OnShoot?.Invoke();
            timeOfLastFiring = Time.time;
        }

    }

    private void Shoot()
    {
        currentWeapon.SpawnBullet(bulletSpawnPos);
        currentWeapon.SpawnMuzzleFlash(bulletSpawnPos);
    }

}
