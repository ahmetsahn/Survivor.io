using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Weapon/Gun")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject muzzleFlash;

    [field : SerializeField] public float FireRate { get; set; }

    [field : SerializeField] public WeaponType WeaponType { get; protected set; }

   
    private void SwapBulletType(string bulletType)
    {
        BulletPool.Instance.ClearPreviousPrefabs();
        BulletPool.Instance.SetPrefabType(bulletType);
    }
    
    private void SwapMuzzleFlashType(string muzzleFlashType)
    {
        MuzzleFlashPool.Instance.ClearPreviousPrefabs();
        MuzzleFlashPool.Instance.SetPrefabType(muzzleFlashType);
    }

    
}
