using System.Collections;
using UnityEngine;

public abstract class BaseShootEffect : MonoBehaviour, IDeathTimer
{
    [field: SerializeField] public float DeathTimer { get; set; }
    

    private void OnEnable()
    {
        StartCoroutine(ReturnToPoolTimer());
    }

    public abstract IEnumerator ReturnToPoolTimer();
    
}
