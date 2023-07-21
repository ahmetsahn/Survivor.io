using System;
using UnityEngine;

public class PlayerCollider : MonoBehaviour, IEnemyCanHit
{

    public void Hit()
    {
        SpawnShootEffect();
    }

    private void SpawnShootEffect()
    {
        var shootEffect = PlayerShootEffectPool.Instance.GetObject();
        shootEffect.transform.position = transform.position;
        shootEffect.gameObject.SetActive(true);
    }

    public void SetShootEffect(string shootEffectType)
    {
        PlayerShootEffectPool.Instance.SetPrefabType(shootEffectType);
    }

}
