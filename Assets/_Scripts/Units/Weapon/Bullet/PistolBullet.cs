using System;
using System.Collections;
using UnityEngine;

public class PistolBullet : BaseBullet
{
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolTimer());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        PistolBulletPool.Instance.ReturnToPool(this);
    }

    public IEnumerator ReturnToPoolTimer()
    {
        yield return new WaitForSeconds(DeathTimer);
        PistolBulletPool.Instance.ReturnToPool(this);
    }
    
}
