using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour, IDeathTimer
{
    [field : SerializeField] public float DeathTimer { get; set; }
    [field: SerializeField] public MuzzleFlashType MuzzleFlashType { get; set; }

    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolTimer());
    }
    
    public IEnumerator ReturnToPoolTimer()
    {
        yield return new WaitForSeconds(DeathTimer);
        MuzzleFlashPool.Instance.ReturnToPool(this);
    }


}
