using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Weapon/Bullet")]
public class BulletStatsSO : ScriptableObject
{
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;

    public int Damage => damage;
    public float Speed => speed;
}
