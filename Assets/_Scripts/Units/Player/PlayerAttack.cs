using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public event Action OnShoot;

    private BaseWeapon currentWeapon;
    [SerializeField] private List<BaseWeapon> weapons;
    [SerializeField] private Transform bulletSpawnPos;
    private float timeOfLastFiring = 0f;

    private void OnEnable()
    {
        BuffManager.OnElectricBuff += SetElectricWeapon;
    }

    private void OnDisable()
    {
        BuffManager.OnElectricBuff -= SetElectricWeapon;
    }

    private void Start()
    {
        SetStartWeapon();
    }

    private void Update()
    {
        if (GameManager.Instance.state == GameStates.Pause) return;
        FireControl();
    }

    public void SetStartWeapon()
    {
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
