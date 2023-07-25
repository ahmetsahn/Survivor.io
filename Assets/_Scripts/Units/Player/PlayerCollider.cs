using System;
using UnityEngine;

public class PlayerCollider
{
    private readonly Transform playerTransform;

    public PlayerCollider(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public void Hit()
    {
        SpawnShootEffect();
    }

    private void SpawnShootEffect()
    {
        var shootEffect = PlayerShootEffectPool.Instance.GetObject();
        shootEffect.transform.position = playerTransform.position;
        shootEffect.gameObject.SetActive(true);
    }

  
}
