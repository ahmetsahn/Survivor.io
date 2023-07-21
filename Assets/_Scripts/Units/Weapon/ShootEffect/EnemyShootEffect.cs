using System.Collections;
using UnityEngine;

public class EnemyShootEffect : BaseShootEffect
{
    public override IEnumerator ReturnToPoolTimer()
    {
        yield return new WaitForSeconds(DeathTimer);
        EnemyShootEffectPool.Instance.ReturnToPool(this);
    }
}
