using System.Collections;
using UnityEngine;

public class PlayerShootEffect : BaseShootEffect
{
    public override IEnumerator ReturnToPoolTimer()
    {
        yield return new WaitForSeconds(DeathTimer);
        PlayerShootEffectPool.Instance.ReturnToPool(this);
    }
}
