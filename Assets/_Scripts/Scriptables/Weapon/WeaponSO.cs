using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/Weapon/Gun")]
public class WeaponSO : ScriptableObject
{
    [field : SerializeField] public float FireRate { get; set; }

    [field : SerializeField] public WeaponType WeaponType { get; protected set; }




}
