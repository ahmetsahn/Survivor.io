using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private List<WeaponSO> weapons;
    [field : SerializeField] public Transform BulletSpawnPos { get; private set; }
    [field: SerializeField] public WeaponSO CurrentWeapon { get; private set; }

    private float timeOfLastFiring = 0f;

    public void SetWeapon(WeaponType weaponType)
    {
        var newWeapon = weapons.Find(weapon => weapon.WeaponType == weaponType);
        if (newWeapon != null)
        {
            CurrentWeapon = newWeapon;
        }
    }

    protected void SpawnBullet(Transform bulletSpawnPos)
    {
        var bullet = BulletPool.Instance.GetObject();
        bullet.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        bullet.gameObject.SetActive(true);
    }
    protected void SpawnMuzzleFlash(Transform bulletSpawnPos)
    {
        var muzzleFlash = MuzzleFlashPool.Instance.GetObject();
        muzzleFlash.transform.SetPositionAndRotation(bulletSpawnPos.position, bulletSpawnPos.rotation);
        muzzleFlash.gameObject.SetActive(true);
    }

    public void FireControl(PlayerAnimation playerAnimation, Transform bulletSpawnPos)
    {
        if (Time.time >= timeOfLastFiring + CurrentWeapon.FireRate)
        {
            Shoot(playerAnimation, bulletSpawnPos);
            timeOfLastFiring = Time.time;
        }

    }

    protected void Shoot(PlayerAnimation playerAnimation, Transform bulletSpawnPos)
    {
        playerAnimation.TopTorso.Play("Shoot");
        SpawnBullet(bulletSpawnPos);
        SpawnMuzzleFlash(bulletSpawnPos);
    }

}
