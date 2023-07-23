using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;

    private void Start()
    {
        if(TryGetComponent(out EnemyHealth health))
        {
            health.OnDeath += PlayDeathAnimation;
        }
    }

    public void PlayDeathAnimation() 
    {
        enemyAnimator.Play("Death");
    }

  
}
