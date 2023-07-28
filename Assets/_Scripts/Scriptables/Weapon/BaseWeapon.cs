using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : ScriptableObject
{
    public WeaponType weaponType;

    public float fireRate;
    public abstract void SpawnBullet(Transform bulletSpawnPos);
    public abstract void SpawnMuzzleFlash(Transform bulletSpawnPos);
}
