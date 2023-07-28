using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void PlayDeathAnimation() 
    {
        animator.SetTrigger("Death");
    }

    public void HandleDeathAnimationEnd()
    {
        EnemyPool.Instance.ReturnToPool(this);
    }

    private void AddListeners()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnDeath += PlayDeathAnimation;
        }
    }

    private void RemoveListeners()
    {
        if (TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.OnDeath -= PlayDeathAnimation;
        }
    }

}
