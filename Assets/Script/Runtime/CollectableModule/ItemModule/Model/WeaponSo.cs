using Script.Runtime.Enum;
using Script.Runtime.Signal;
using Script.Runtime.WeaponModule;
using UnityEngine;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.Model
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/WeaponData")]
    public class WeaponSo : ItemSo
    {
        [SerializeField]
        private PlayerWeaponSo playerWeaponSo;

        public override PlayerWeaponSo GetWeaponSo()
        {
            return playerWeaponSo;
        }
    }
}