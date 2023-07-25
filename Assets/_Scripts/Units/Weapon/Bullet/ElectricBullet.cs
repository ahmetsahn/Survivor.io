using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBullet : BaseBullet
{
    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolTimer());
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.right * (bulletStats.Speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        ElectricBulletPool.Instance.ReturnToPool(this);
    }

    public IEnumerator ReturnToPoolTimer()
    {
        yield return new WaitForSeconds(DeathTimer);
        ElectricBulletPool.Instance.ReturnToPool(this);
    }
}
