using Script.Runtime.CollectableModule.ItemModule.Model;
using Script.Runtime.WeaponModule;

namespace Script.Runtime.Signal
{
    public readonly struct SetPlayerWeaponSignal
    {
        public readonly PlayerWeaponSo PlayerWeaponSo;
        
        public SetPlayerWeaponSignal(PlayerWeaponSo playerWeaponSo)
        {
            PlayerWeaponSo = playerWeaponSo;
        }
    }
}