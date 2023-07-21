using UnityEngine;

public class MuzzleFlashPool : BaseObjectPool<MuzzleFlash>
{
    public override void SetPrefabType(string Type)
    {
        currentPrefab = prefabs.Find(x => x.MuzzleFlashType.ToString() == Type);
    }
}
