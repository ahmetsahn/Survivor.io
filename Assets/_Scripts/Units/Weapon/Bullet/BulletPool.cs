using UnityEngine;

public class BulletPool : BaseObjectPool<Bullet>
{
    public override void SetPrefabType(string Type)
    {
        currentPrefab = prefabs.Find(x => x.BulletType.ToString() == Type);
    }
}
